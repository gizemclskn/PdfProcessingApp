# PdfProcessingApp

## Genel Bakış
**PdfProcessingApp**, PDF belgelerini yönetmek ve işlemek için geliştirilmiş bir .NET uygulamasıdır.  
Uygulama, PDF dosyalarını yükleme, **anahtar kelimelere göre içeriği bölümlere ayırma**, anahtar kelime yönetimi ve PDF oluşturma olaylarının kaydedilmesi gibi işlevler sunar.  
Proje, **Entity Framework Core** kullanılarak veri yönetimi sağlar ve **repository (depo) deseni** ile yapılandırılmıştır.

---

## Özellikler
- **PDF belgelerini yükleme ve depolama.**
- **PDF içeriğini anahtar kelimelere göre bölümlere ayırma.**
- **Belge bölümlerine bağlı anahtar kelimeleri yönetme.**
- **PDF oluşturma olaylarını kaydetme.**
- **PDF belgelerinin detaylarını alma ve güncelleme.**

---

## Proje Mimarisi

### **Model Katmanı**
- Uygulamada kullanılan veri yapıları ve ilişkilerin tanımlandığı katmandır.

### **Veri Erişim Katmanı (DAL)**
- Veritabanı veya dosya sistemiyle etkileşimi sağlayan CRUD işlemleri ve log kayıtlarının yönetildiği katmandır.

### **İş Mantığı Katmanı (BLL)**
- PDF içeriğini bölümlere ayırma ve anahtar kelime yönetimi gibi uygulamanın temel iş mantıklarını içeren katmandır.

### **Sunum Katmanı**
- Kullanıcı arayüzü veya API uç noktalarını yöneten katmandır.


## Ana Bileşenler

### **AppDbContext**
- **Entity Framework Core** kullanılarak veritabanı işlemlerini ve varlık ilişkilerini yönetir.

### **Repository (Depo)**
- CRUD işlemleri ve log yönetimi için veri erişim yöntemlerini uygular.

### **PdfManager**
- **Anahtar kelimelere** göre PDF içeriğini bölümlere ayırır ve yeni PDF belgeleri oluşturur.
  - **Anahtar Kelime Eşleştirme**: Belge bölümlerine anahtar kelime ekleme.
  - **Yeni PDF Oluşturma**: Anahtar kelimelere göre PDF’leri bölümlendirip kaydetme.

## Kurulum ve Kullanım

### Gereksinimler
- .NET 6 veya üzeri.
- Entity Framework Core.

### Adımlar
1. Depoyu klonlayın:
   ```bash
   git clone https://github.com/your-repository/PdfProcessingApp.git
