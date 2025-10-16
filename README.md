# 📅 Zaman Kullanımı Raporlama Sistemi (Time Usage Reporting System)

## 🎯 Proje Hakkında

Bu proje, çalışanların çalışma saatlerini, görevlerini ve projelerdeki zaman kullanımlarını takip etmek için tasarlanmış bir Kurumsal Kaynak Planlama (ERP) modülüdür. Sistem, modern bir yazılım mimarisi olan **Clean Architecture** prensiplerine uygun olarak tasarlanmış olup, yetkilendirme için **JWT (JSON Web Token)** kullanır.

## 🚀 Teknolojiler ve Mimari

Bu projenin omurgasını .NET ve Clean Architecture oluşturmaktadır.

| Kategori | Teknoloji / Kütüphane | Açıklama |
| :--- | :--- | :--- |
| **Mimari** | Clean Architecture (Katmanlı Mimari) | Sorumlulukların ayrılması ve bağımsız test edilebilirlik. |
| **API** | .NET 8 / ASP.NET Core Web API | Güvenli ve yüksek performanslı backend hizmetleri. |
| **Veritabanı** | Microsoft SQL Server | İlişkisel veritabanı. |
| **ORM** | Entity Framework Core (EF Core) | Veritabanı işlemleri ve haritalamalar. |
| **Güvenlik** | JWT (JSON Web Token) | Kimlik doğrulama ve yetkilendirme. |
| **Özel Güvenlik** | Kriptografik Alanlar | Çalışan Ad/Soyad gibi hassas veriler veritabanında şifreli saklanır. |
| **Veri Yönetimi** | Soft Delete (Mantıksal Silme) | Veritabanından fiziksel silme yerine mantıksal silme kullanılır. |

## 📂 Katman Yapısı

Proje, bağımlılıkları yönetmek için aşağıdaki katmanlardan oluşur:

1.  **`Core`**: Temel varlıklar, güvenlik yapıları (JWT, OperationClaim) ve genel yardımcı sınıflar.
2.  **`Domain`**: İş alanına özgü varlıklar (`Employee`, `Department`, `TimeLog`).
3.  **`Application`**: İş mantığı (Business Logic), Komutlar, Sorgular, DTO'lar ve Handler'lar.
4.  **`Persistence`**: Veri erişim katmanı. `DbContext`, EF Core Konfigürasyonları ve Repository uygulamaları.
5.  **`API` (Presentation)**: API Controller'ları, Swagger ve bağımlılık enjeksiyonu (DI) ayarları.

Güvenlik Anahtarı (User Secrets) kullanışmıştır.
