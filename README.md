# Mikroservis Demo Projesi

Bu proje, .NET 9 ile oluşturulmuş bir mikroservis mimarisini gösteren kapsamlı bir demo projesidir. Kullanıcıların kurslara göz atabildiği, sepete ekleyebildiği, indirim uygulayabildiği, sipariş verebildiği ve ödeme yapabildiği basit bir e-öğrenme platformunu simüle eder.

## Mimariye Genel Bakış

Proje, Alan Adı Odaklı Tasarım (DDD) yaklaşımıyla tasarlanmış olup, ölçeklenebilir ve dayanıklı bir sistem oluşturmak için çeşitli desenler ve teknolojiler kullanır.

- **API Gateway (YARP):** Tüm istemci istekleri için tek bir giriş noktasıdır. Trafiği uygun mikroservise yönlendirir ve kimlik doğrulama gibi kesişen ilgileri yönetir.
- **Kimlik ve Erişim Yönetimi (Keycloak):** Tüm servisler için merkezi kimlik doğrulama ve yetkilendirme sağlar.
- **Senkron İletişim:** Servisler, anlık yanıt gerektiren durumlar için doğrudan HTTP istekleri aracılığıyla iletişim kurar (örneğin, Sipariş servisinin Ödeme servisini çağırması). Bu iletişim `Refit` kullanılarak gerçekleştirilmiştir.
- **Asenkron İletişim (RabbitMQ):** Servisler, olay tabanlı iş akışları için bir mesajlaşma sistemi (message bus) üzerinden iletişim kurar. Bu, servisler arasında gevşek bağlılık ve dayanıklılık sağlar (örneğin, bir sipariş oluşturulduğunda sepeti temizlemek ve yeni bir indirim oluşturmak için olaylar yayınlanır). Bu iletişim `MassTransit` kullanılarak gerçekleştirilmiştir.
- **CQRS & MediatR:** Her servis, okuma ve yazma işlemlerini ayırmak için Komut ve Sorgu Sorumluluğunu Ayırma (CQRS) desenini kullanır. Bu, `MediatR` kütüphanesi ile uygulanmıştır.
- **Her Servise Ayrı Veritabanı:** Her mikroservis, veri katmanında sıkı bağlılığı önlemek için kendi özel veritabanına sahiptir.

### Mikroservisler

- **Catalog API:** Kursları ve kategorileri yönetir.
- **Basket API:** Kullanıcı alışveriş sepetlerini yönetir.
- **Discount API:** İndirim kuponlarını yönetir.
- **Order API:** Sipariş oluşturma sürecini yönetir.
- **Payment API:** Ödeme sürecini simüle eder.
- **File API:** Dosya yükleme işlemlerini yönetir (örneğin, kurs resimleri).
- **Gateway:** İstekleri diğer servislere yönlendiren ana giriş noktasıdır.

## Kullanılan Teknolojiler

- **Backend:** .NET 9, ASP.NET Core (Minimal API'ler)
- **Veritabanları:**
  - MongoDB (Catalog ve Discount servisleri için)
  - Redis (Basket servisi için)
  - MS SQL Server (Order servisi için)
  - PostgreSQL (Keycloak verileri için)
- **Altyapı:**
  - Docker & Docker Compose
  - RabbitMQ (Mesajlaşma Sistemi)
  - Keycloak (Kimlik ve Erişim Yönetimi)
  - YARP (API Ağ Geçidi)
- **Kütüphaneler ve Desenler:**
  - MediatR (CQRS Deseni)
  - MassTransit (RabbitMQ Soyutlaması)
  - Entity Framework Core (SQL Server ve MongoDB için)
  - Refit (Tip-güvenli REST istemcisi)
  - FluentValidation (İstek Doğrulama)
  - AutoMapper (Nesne Eşleme)

## Kurulum ve Başlatma

### Ön Gereksinimler

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### 1. Ortam Değişkenlerini Ayarlama

Projenin kök dizininde, size sağlanan dosyayı kopyalayarak bir `.env` dosyası oluşturun. Tüm varsayılan değerler yerel geliştirme ortamı için ayarlanmıştır.

```env
# .env
MONGO_USERNAME=username
MONGO_PASSWORD=password
REDIS_PASSWORD=Password12
REDIS_UI_USERNAME=myuser
REDIS_UI_PASSWORD=Password12
SA_PASSWORD=Password12*
KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=password
POSTGRES_DB=keycloak_db
POSTGRES_USER=keycloak_db_user
POSTGRES_PASSWORD=keycloak_db_user_password
RABBITMQ_DEFAULT_USER=root
RABBITMQ_DEFAULT_PASS=Password12
```

### 2. Uygulamayı Çalıştırma

Projenin kök dizininde bir terminal açın ve tüm servisleri derleyip başlatmak için aşağıdaki komutu çalıştırın:

```bash
docker-compose up -d
```

Bu komut, tüm mikroservisleri ve bağımlılıklarını arka planda (detached modda) başlatacaktır.

### 3. Keycloak Kurulumu

Servisleri başlattıktan sonra, kimlik doğrulama için Keycloak'u yapılandırmanız gerekir.

1.  Keycloak yönetici konsoluna gidin: `http://localhost:8080`
2.  `.env` dosyanızdaki kimlik bilgileriyle (`KEYCLOAK_ADMIN` ve `KEYCLOAK_ADMIN_PASSWORD`) giriş yapın.
3.  `DemoTenant` adında yeni bir realm oluşturun.
4.  `DemoTenant` realm'ı içinde her mikroservis ve ağ geçidi için istemciler (clients) oluşturun:
    - `catalog.api`
    - `basket.api`
    - `discount.api`
    - `order.api`
    - `payment.api`
    - `file.api`
    - `gateway.api`
5.  Test senaryolarınız için istemci kapsamlarını, rolleri ve kullanıcıları gerektiği gibi yapılandırın. Makineden makineye iletişim için (örn. Order API'den Payment API'ye) Client Credentials akışını kullanın. Kullanıcıya yönelik uç noktalar için Resource Owner Password akışını kullanın.

### Servis Portları

Her şey çalışır duruma geldiğinde, servislere ve arayüzlere aşağıdaki adreslerden erişebilirsiniz:

| Servis                       | URL                             | Açıklama                                    |
| ---------------------------- | ------------------------------- | ------------------------------------------- |
| **API Gateway**              | `http://localhost:5117`         | Tüm API'ler için tek giriş noktası.         |
| Catalog API                  | `http://localhost:5090`         | Kursları ve kategorileri yönetir.           |
| Basket API                   | `http://localhost:5020`         | Alışveriş sepetlerini yönetir.              |
| Discount API                 | `http://localhost:5252`         | İndirim kodlarını yönetir.                  |
| Order API                    | `http://localhost:5134`         | Sipariş işlemlerini yönetir.                |
| Payment API                  | `http://localhost:5266`         | Ödeme işlemlerini simüle eder.              |
| File API                     | `http://localhost:5133`         | Dosya yükleme işlemlerini yönetir.          |
| **Keycloak Admin**           | `http://localhost:8080`         | Kimlik ve Erişim Yönetimi Konsolu.          |
| **RabbitMQ Management UI**   | `http://localhost:15672`        | Mesajlaşma Sistemi Yönetim Arayüzü.         |
| **Mongo Express (Catalog)**  | `http://localhost:27032`        | Catalog veritabanı için arayüz.             |
| **Mongo Express (Discount)** | `http://localhost:27036`        | Discount veritabanı için arayüz.            |
| **Redis Commander (Basket)** | `http://localhost:27033`        | Basket veritabanı için arayüz.              |
| **pgAdmin (Keycloak)**       | `http://localhost:8888`         | Keycloak'un PostgreSQL veritabanı için arayüz. |

---

# Microservices Demo Project

This is a comprehensive demo project that demonstrates a microservices architecture built with .NET 9. It simulates a simple e-learning platform where users can browse courses, add them to a basket, apply discounts, place orders, and make payments.

## Architecture Overview

The project is designed with a Domain-Driven Design (DDD) approach and utilizes various patterns and technologies to create a scalable and resilient system.

- **API Gateway (YARP):** A single entry point for all client requests. It routes traffic to the appropriate microservice and handles cross-cutting concerns like authentication.
- **Identity & Access Management (Keycloak):** Centralized authentication and authorization for all services.
- **Synchronous Communication:** Services communicate directly via HTTP requests for immediate responses (e.g., Order service calling Payment service). This is implemented using `Refit`.
- **Asynchronous Communication (RabbitMQ):** Services communicate via a message bus for event-driven workflows, ensuring loose coupling and resilience (e.g., when an order is created, events are published to clear the basket and generate a new discount). This is implemented using `MassTransit`.
- **CQRS & MediatR:** Each service internally uses the Command and Query Responsibility Segregation (CQRS) pattern to separate read and write operations, implemented with the `MediatR` library.
- **Database Per Service:** Each microservice has its own dedicated database, preventing tight coupling at the data layer.

### Microservices

- **Catalog API:** Manages courses and categories.
- **Basket API:** Manages user shopping baskets.
- **Discount API:** Manages discount coupons.
- **Order API:** Manages the order creation process.
- **Payment API:** Simulates the payment process.
- **File API:** Handles file uploads (e.g., course images).
- **Gateway:** The main entry point that routes requests to other services.

## Technologies Used

- **Backend:** .NET 9, ASP.NET Core (Minimal APIs)
- **Databases:**
  - MongoDB (for Catalog and Discount services)
  - Redis (for Basket service)
  - MS SQL Server (for Order service)
  - PostgreSQL (for Keycloak data)
- **Infrastructure:**
  - Docker & Docker Compose
  - RabbitMQ (Message Bus)
  - Keycloak (Identity & Access Management)
  - YARP (API Gateway)
- **Libraries & Patterns:**
  - MediatR (CQRS Pattern)
  - MassTransit (RabbitMQ Abstraction)
  - Entity Framework Core (for SQL Server & MongoDB)
  - Refit (Type-safe REST client)
  - FluentValidation (Request Validation)
  - AutoMapper (Object Mapping)

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### 1. Configure Environment Variables

Create a `.env` file in the root directory by copying the provided one. All default values are set for a local development environment.

```env
# .env
MONGO_USERNAME=username
MONGO_PASSWORD=password
REDIS_PASSWORD=Password12
REDIS_UI_USERNAME=myuser
REDIS_UI_PASSWORD=Password12
SA_PASSWORD=Password12*
KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=password
POSTGRES_DB=keycloak_db
POSTGRES_USER=keycloak_db_user
POSTGRES_PASSWORD=keycloak_db_user_password
RABBITMQ_DEFAULT_USER=root
RABBITMQ_DEFAULT_PASS=Password12
```

### 2. Run the Application

Open a terminal in the project's root directory and run the following command to build and start all services:

```bash
docker-compose up -d
```

This will start all microservices and their dependencies in detached mode.

### 3. Keycloak Setup

After starting the services, you need to configure Keycloak for authentication.

1.  Navigate to the Keycloak admin console: `http://localhost:8080`
2.  Log in with the credentials from your `.env` file (`KEYCLOAK_ADMIN` and `KEYCLOAK_ADMIN_PASSWORD`).
3.  Create a new realm named `DemoTenant`.
4.  Inside the `DemoTenant` realm, create clients for each microservice and the gateway:
    - `catalog.api`
    - `basket.api`
    - `discount.api`
    - `order.api`
    - `payment.api`
    - `file.api`
    - `gateway.api`
5.  Configure client scopes, roles, and users as needed for your testing scenarios. For machine-to-machine communication (e.g., Order API to Payment API), use the Client Credentials flow. For user-facing endpoints, use the Resource Owner Password flow.

### Service Ports

Once everything is running, you can access the services and UIs at the following addresses:

| Service                       | URL                             | Description                                 |
| ----------------------------- | ------------------------------- | ------------------------------------------- |
| **API Gateway**               | `http://localhost:5117`         | The single entry point for all APIs.        |
| Catalog API                   | `http://localhost:5090`         | Manages courses and categories.             |
| Basket API                    | `http://localhost:5020`         | Manages shopping baskets.                   |
| Discount API                  | `http://localhost:5252`         | Manages discount codes.                     |
| Order API                     | `http://localhost:5134`         | Handles order processing.                   |
| Payment API                   | `http://localhost:5266`         | Simulates payment processing.               |
| File API                      | `http://localhost:5133`         | Handles file uploads.                       |
| **Keycloak Admin**            | `http://localhost:8080`         | Identity and Access Management Console.     |
| **RabbitMQ Management UI**    | `http://localhost:15672`        | Message Bus Management UI.                  |
| **Mongo Express (Catalog)**   | `http://localhost:27032`        | UI for Catalog DB.                          |
| **Mongo Express (Discount)**  | `http://localhost:27036`        | UI for Discount DB.                         |
| **Redis Commander (Basket)**  | `http://localhost:27033`        | UI for Basket DB.                           |
| **pgAdmin (Keycloak)**        | `http://localhost:8888`         | UI for Keycloak's PostgreSQL DB.            |
