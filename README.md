## English

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

## Secure deployment / Güvenli yayınlama

The repository does not contain production credentials. Copy `.env.example` to an untracked `.env` only for local Docker usage, or define the same variables in the secret/environment settings of the hosting platform. Never place real values in `.env.example`, Dockerfiles, Compose files, frontend variables, README files, or GitHub Actions workflow files.

Bu repository production giriş bilgilerini içermez. Yerel Docker kullanımı için `.env.example` dosyasını Git tarafından takip edilmeyen `.env` olarak oluşturun veya aynı değişkenleri hosting platformunun secret/environment alanında tanımlayın. Gerçek değerleri `.env.example`, Dockerfile, Compose, frontend değişkenleri, README veya GitHub Actions dosyalarına yazmayın.

Required secrets / Zorunlu secretlar:

```text
POSTGRES_USER
POSTGRES_PASSWORD
JWT_KEY
LUCKYPENNY_LICENSE_KEY
```

Optional public settings / Opsiyonel genel ayarlar:

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

Production Docker start / Production Docker başlatma:

```powershell
Copy-Item .env.example .env
# Replace the secret placeholders in .env before continuing.
docker compose up -d --build
docker compose ps
```

Only the frontend port is published. Browser requests to `/api` are proxied to the internal backend container, and PostgreSQL is reachable only from the Docker network. For separate frontend/backend hosting, set `VITE_API_URL` to the public API URL and add the frontend origin as `Cors__AllowedOrigins__0` in the backend environment.

AutoMapper 15+ requires a license key for production use. Eligible individuals and small organizations can obtain a free Community license. Store it only as `LUCKYPENNY_LICENSE_KEY` in `.env` or the hosting platform's secret manager; never commit the real key.

Yalnızca frontend portu dışarı açılır. Tarayıcının `/api` istekleri iç ağdaki backend containerına yönlendirilir ve PostgreSQL yalnızca Docker ağı içinden erişilebilir. Frontend ve backend ayrı servislerde yayınlanacaksa `VITE_API_URL` değerini genel API adresi yapın ve frontend originini backend ortamında `Cors__AllowedOrigins__0` olarak tanımlayın.
