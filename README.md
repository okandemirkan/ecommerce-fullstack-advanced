## English

## About This Version

This repository is the enhanced and deployment-ready version of the previously published [ecommerce-fullstack](https://github.com/okandemirkan/ecommerce-fullstack) project.

**Live Demo:** [Visit the application](LIVE_SITE_URL)

This version introduces an isolated **Demo Workspace** architecture designed to keep the public demonstration safe and reusable. Whenever a visitor starts a Demo Admin session, the application creates a temporary workspace containing isolated copies of the demo users, products, categories, orders, addresses, reviews, and related data.

All changes made during that session, including creating, updating, deleting, or restoring records, are limited to the visitor's own workspace. Therefore, actions performed by one visitor cannot affect another visitor or modify the original seed data. Expired demo workspaces are automatically cleaned up by the application.

The project also includes production-oriented improvements such as Dockerized deployment, environment-based secret management, PostgreSQL persistence, responsive administration interfaces, bilingual Turkish/English support, health checks, security headers, rate limiting, CI verification, and hardened container configuration.

This application was developed as a portfolio and learning project to demonstrate full-stack development with .NET, React, Clean Architecture, CQRS, MediatR, Entity Framework Core, PostgreSQL, and Docker.

---

### Full-Stack E-Commerce Application

This project is a full-stack e-commerce application developed as a learning-focused project to improve my skills in RESTful API development, backend architecture, and modern frontend development.

The backend was built with **.NET and ASP.NET Core Web API**, following **Clean Architecture** principles. It uses architectural patterns and technologies such as **CQRS**, **MediatR**, **Entity Framework Core**, repository abstractions, dependency injection, JWT-based authentication and role-based authorization. **PostgreSQL** is used as the relational database.

The frontend was developed with **React and Vite** and provides responsive customer and admin interfaces. It includes product and category management, authentication, shopping cart operations, order processing, address management, reviews, profile management, multilingual Turkish and English support, and administrative workflows.

During frontend development, I benefited from **ChatGPT and Claude** as AI-assisted development tools, particularly for brainstorming UI approaches, debugging, code review, and improving the user experience. I reviewed, adapted, integrated, and tested the resulting implementations according to the requirements and architecture of the project.

The application is containerized using **Docker and Docker Compose**, allowing the frontend, backend, and PostgreSQL database to run together in a consistent development environment.

This repository represents an ongoing learning journey focused on:

- Building maintainable RESTful APIs with .NET
- Applying Clean Architecture and CQRS
- Designing authentication and authorization workflows
- Developing responsive React applications
- Connecting frontend and backend systems
- Working with relational databases and Entity Framework Core
- Containerizing full-stack applications with Docker
- Using AI tools responsibly as part of the software development workflow

> This application is an educational and portfolio project. It is not intended to be used as a production-ready commercial e-commerce platform without additional security, testing, monitoring, and infrastructure improvements.

---

## Türkçe

## Bu Sürüm Hakkında

Bu repository, daha önce yayınlanan [ecommerce-fullstack](https://github.com/okandemirkan/ecommerce-fullstack) projesinin geliştirilmiş ve yayına hazır hale getirilmiş sürümüdür.

**Canlı Demo:** [Uygulamayı ziyaret et](LIVE_SITE_URL)

Bu sürümde, herkese açık demo ortamını güvenli ve tekrar kullanılabilir tutmak amacıyla izole bir **Demo Workspace** mimarisi geliştirilmiştir. Bir ziyaretçi Demo Admin oturumu başlattığında; demo kullanıcılarının, ürünlerin, kategorilerin, siparişlerin, adreslerin, değerlendirmelerin ve ilişkili verilerin izole kopyalarını içeren geçici bir çalışma alanı oluşturulur.

Oturum sırasında gerçekleştirilen kayıt ekleme, güncelleme, silme veya geri yükleme işlemlerinin tamamı yalnızca ilgili ziyaretçinin workspace alanını etkiler. Böylece bir ziyaretçinin yaptığı değişiklikler başka ziyaretçileri etkilemez ve projenin orijinal seed verileri değiştirilmez. Süresi dolan demo workspace alanları uygulama tarafından otomatik olarak temizlenir.

Projeye ayrıca Docker tabanlı yayınlama, ortam değişkenleriyle secret yönetimi, PostgreSQL veri kalıcılığı, responsive admin arayüzleri, Türkçe/İngilizce dil desteği, health check'ler, güvenlik başlıkları, rate limiting, CI doğrulamaları ve güçlendirilmiş container yapılandırmaları eklenmiştir.

Bu uygulama; .NET, React, Clean Architecture, CQRS, MediatR, Entity Framework Core, PostgreSQL ve Docker teknolojileriyle full-stack geliştirme yetkinliklerini göstermek amacıyla hazırlanmış bir portföy ve öğrenme projesidir.

### Full-Stack E-Ticaret Uygulaması

Bu proje; RESTful API geliştirme, backend mimarisi ve modern frontend geliştirme alanlarındaki yetkinliklerimi artırmak amacıyla hazırladığım, öğrenme odaklı bir full-stack e-ticaret uygulamasıdır.

Backend tarafı **.NET ve ASP.NET Core Web API** kullanılarak, **Clean Architecture** prensiplerine uygun biçimde geliştirilmiştir. Projede **CQRS**, **MediatR**, **Entity Framework Core**, repository soyutlamaları, dependency injection, JWT tabanlı kimlik doğrulama ve rol tabanlı yetkilendirme gibi mimari yaklaşımlar ve teknolojiler kullanılmıştır. İlişkisel veritabanı olarak **PostgreSQL** tercih edilmiştir.

Frontend tarafı **React ve Vite** ile geliştirilmiş, responsive kullanıcı ve admin arayüzleri hazırlanmıştır. Ürün ve kategori yönetimi, kullanıcı işlemleri, sepet, sipariş oluşturma ve takip, adres yönetimi, değerlendirmeler, profil yönetimi, Türkçe ve İngilizce dil desteği ile yönetim paneli iş akışları projede yer almaktadır.

Frontend geliştirme sürecinde **ChatGPT ve Claude** yapay zekâ araçlarından; arayüz fikirleri üretme, hata ayıklama, kod inceleme ve kullanıcı deneyimini geliştirme konularında destek aldım. Ortaya çıkan uygulamaları projenin ihtiyaçlarına ve mimarisine göre inceleyerek uyarladım, entegre ettim ve test ettim.

Uygulama **Docker ve Docker Compose** kullanılarak container haline getirilmiştir. Böylece frontend, backend ve PostgreSQL veritabanı tutarlı bir geliştirme ortamında birlikte çalıştırılabilmektedir.

Bu repository aşağıdaki konulara odaklanan devam eden bir öğrenme sürecini temsil etmektedir:

- .NET ile sürdürülebilir RESTful API geliştirme
- Clean Architecture ve CQRS uygulama
- Kimlik doğrulama ve yetkilendirme süreçleri tasarlama
- Responsive React uygulamaları geliştirme
- Frontend ve backend sistemlerini entegre etme
- Entity Framework Core ve ilişkisel veritabanlarıyla çalışma
- Full-stack uygulamaları Docker ile container haline getirme
- Yapay zekâ araçlarını yazılım geliştirme sürecinde bilinçli şekilde kullanma

> Bu uygulama eğitim ve portföy amacıyla geliştirilmiştir. Ek güvenlik, test, izleme ve altyapı geliştirmeleri yapılmadan production ortamında ticari bir e-ticaret platformu olarak kullanılması amaçlanmamıştır.

---

## Secure deployment

The repository does not contain production credentials. Copy `.env.example` to an untracked `.env` only for local Docker usage, or define the same variables in the secret/environment settings of the hosting platform. Never place real values in `.env.example`, Dockerfiles, Compose files, frontend variables, README files, or GitHub Actions workflow files.

Bu repository production giriş bilgilerini içermez. Yerel Docker kullanımı için `.env.example` dosyasını Git tarafından takip edilmeyen `.env` olarak oluşturun veya aynı değişkenleri hosting platformunun secret/environment alanında tanımlayın. Gerçek değerleri `.env.example`, Dockerfile, Compose, frontend değişkenleri, README veya GitHub Actions dosyalarına yazmayın.

Required secrets:

```text
POSTGRES_USER
POSTGRES_PASSWORD
JWT_KEY
LUCKYPENNY_LICENSE_KEY
```

Optional public settings:

```text
POSTGRES_DB
JWT_ISSUER
JWT_AUDIENCE
JWT_EXPIRES_IN_MINUTES
DEMO_WORKSPACE_LIFETIME_MINUTES
APP_PORT
VITE_API_URL
```

Generate a JWT key with at least 32 random bytes. PowerShell example:

```powershell
$bytes = New-Object byte[] 48
[Security.Cryptography.RandomNumberGenerator]::Fill($bytes)
[Convert]::ToBase64String($bytes)
```

Production Docker start:

```powershell
Copy-Item .env.example .env
# Replace the secret placeholders in .env before continuing.
docker compose up -d --build
docker compose ps
```

Only the frontend port is published. Browser requests to `/api` are proxied to the internal backend container, and PostgreSQL is reachable only from the Docker network. For separate frontend/backend hosting, set `VITE_API_URL` to the public API URL and add the frontend origin as `Cors__AllowedOrigins__0` in the backend environment.

AutoMapper 15+ requires a license key for production use. Eligible individuals and small organizations can obtain a free Community license. Store it only as `LUCKYPENNY_LICENSE_KEY` in `.env` or the hosting platform's secret manager; never commit the real key.

Yalnızca frontend portu dışarı açılır. Tarayıcının `/api` istekleri iç ağdaki backend containerına yönlendirilir ve PostgreSQL yalnızca Docker ağı içinden erişilebilir. Frontend ve backend ayrı servislerde yayınlanacaksa `VITE_API_URL` değerini genel API adresi yapın ve frontend originini backend ortamında `Cors__AllowedOrigins__0` olarak tanımlayın.

## Güvenli Yayınlama

Bu repository production ortamına ait gerçek giriş bilgilerini veya gizli anahtarları içermez. Yerel Docker kullanımı için `.env.example` dosyasını Git tarafından takip edilmeyen bir `.env` dosyasına kopyalayın. Bir hosting platformu kullanıyorsanız aynı değişkenleri platformun secret/environment ayarlarında tanımlayın.

Gerçek değerleri `.env.example`, Dockerfile, Docker Compose, frontend değişkenleri, README veya GitHub Actions workflow dosyalarına kesinlikle yazmayın.

Zorunlu secretlar:

```text
POSTGRES_USER
POSTGRES_PASSWORD
JWT_KEY
LUCKYPENNY_LICENSE_KEY
```

Opsiyonel genel ayarlar:

```text
POSTGRES_DB
JWT_ISSUER
JWT_AUDIENCE
JWT_EXPIRES_IN_MINUTES
DEMO_WORKSPACE_LIFETIME_MINUTES
APP_PORT
VITE_API_URL
```

En az 32 rastgele byte içeren bir JWT anahtarı oluşturun. PowerShell örneği:

```powershell
$bytes = New-Object byte[] 48
$rng = [Security.Cryptography.RandomNumberGenerator]::Create()
$rng.GetBytes($bytes)
$rng.Dispose()
[Convert]::ToBase64String($bytes)
```

Production ortamını Docker ile başlatma:

```powershell
Copy-Item .env.example .env
# Devam etmeden önce .env içerisindeki örnek secret değerlerini değiştirin.
docker compose up -d --build
docker compose ps
```

Yalnızca frontend portu dışarı açılır. Tarayıcıdan gönderilen `/api` istekleri, Docker ağı içerisindeki backend containerına yönlendirilir. PostgreSQL’e yalnızca Docker ağı içerisinden erişilebilir.

Frontend ve backend ayrı hosting servislerinde yayınlanacaksa `VITE_API_URL` değerini backend servisinin genel API adresi olarak ayarlayın. Ayrıca frontend adresini backend ortamında `Cors__AllowedOrigins__0` değişkeniyle izin verilen originler arasına ekleyin.

AutoMapper 15 ve üzeri sürümlerin production ortamında kullanılması için lisans anahtarı gerekir. Uygun bireysel geliştiriciler ve küçük kuruluşlar ücretsiz Community lisansı alabilir.

Gerçek lisans anahtarını yalnızca `.env` dosyasındaki `LUCKYPENNY_LICENSE_KEY` alanında veya hosting platformunun secret yönetim sisteminde saklayın. Gerçek lisans anahtarını hiçbir zaman Git repository’sine commit etmeyin.
