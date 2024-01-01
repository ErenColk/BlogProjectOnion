# BlogProjectOnion
  <p>Blog Yönetim Sistemi, kullanıcılara üye ve yazar olarak iki ayrı rolde katılma imkanı tanıyan bir ASP.NET MVC Core uygulamasıdır. Üyeler, sisteme kayıt olarak yazarları takip edebilir, yayınlanan makaleleri okuyabilir. Ayrıca, yazarlar kendi makalelerini oluşturabilir ve paylaşabilirler. Takip etme özelliği sayesinde kullanıcılar, ilgi duydukları yazarları ve konuları daha yakından takip edebilirler.</p>
  <p>Ayrıca, sistemin yöneticileri için özel bir Admin Paneli bulunmaktadır. Admin Paneli üzerinden site içindeki kullanıcıları yönetebilir, istenmeyen içerikleri denetleyebilir ve genel sistemi kontrol edebilirsiniz. Bu sayede, site üzerindeki etkileşimleri izlemek ve yönetmek daha kolay hale gelir.</p>

# Projede Öne Çıkanlar 

**Performans Odaklı Tasarım:** Projede, performansı artırmak adına AJAX yapısını kullanarak daha kullanılabilir bir deneyim sunmaya özen gösterdim. Ayrıca, tasarım kısmında kullanıcı dostu bir arayüz ve açık renkler tercih ettim.

**Güvenlik ve Kimlik Doğrulama:** Admin panelini, Area yapısıyla özelleştirdim ve kimlik doğrulama işlemleri için Identity yapısını kullandım. Ayrıca, kayıt olma esnasında  mail üzerinden gelen kodlarla güvenli bir kayıt sürecini MailKit kütüphanesi ile sağladım.

**Validasyon İşlemleri:** Fluent Validation kütüphanesinden faydalanarak, veri girişlerinde doğrulama işlemlerini daha etkili bir şekilde yönettim. Projenin büyük bir kısmında validation işlemleri sağlandı.

**Grafik İşlemleri:** Projede, verilerin daha etkili bir şekilde görselleştirilmesi amacıyla grafikler kullanılarak yönetici panelinde çeşitli analitik veri gösterimleri sağlandı. Bu işlemde Chart kontrolü kullanılarak veri setleri grafiklere dönüştürüldü.

# Projeyi Çalıştırma Adımları:

**1.ADIM :** Projeyi localde çalıştırabilmek için Clone edebilir veya Code kısmında yazan Open with Visual Studio yazan sekmeye tıklayarak projeyi locale taşıyabilirsiniz. 

![image](https://github.com/ErenColk/BlogProjectOnion/assets/137501644/eb84befc-3e1c-46a4-96b5-cfce20686d8d)

**2.ADIM :** İndirilen projede appsettings.json dosyasına gelerek uygulamanın veritabanına bağlanmak için kullanacağı bağlantı dizesini şekildeki gibi doldurmanız gerekiyor.

```js
 "AllowedHosts": "*",
 "ConnectionStrings": {
   "conStr":"Server=[Server adı];Database=[Database adı];Uid=[Username];Pwd=[Parola] "
 }
```
**3.ADIM :**  Set as Startup Project olarak **"Presentation"** katmanını seçip Package Manager Console açıp Default Project olarak **"Infrastructure"** katmanını seçmelisiniz.  

**4.SON ADIM :**  Package Console Manager kısmına aşağıdaki belirtilen kodları sırasıyla yazmalısınız. Artık projeyi çalıştırabilirsiniz.

```js
 add-migration [Migration adı]


```

```js
 update-database
```
# SİTEYE AİT GÖRSELLER
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/a479ed88-5a8d-4653-acc6-7c0fd74a6cd8" alt="HomePage">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/ed47e624-cbab-4ad6-a978-2c34ab820ade" alt="About">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/c20fdf84-8c60-4a6c-bcf4-5d52b45db5c7" alt="Login">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/ee420d1e-9863-4528-971e-3fa770a9e180" alt="Register">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/662ccb54-bf5d-46ce-9983-f0d64353665e" alt="AdminArticlePage">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/2afdec4f-3656-4152-8c73-3de6d7940040" alt="AddArticle">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/e1086ff4-5487-4198-b34f-ca6a0ee6a0be" alt="ArticleDetail">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/7ac7b645-bb9e-43fb-9189-c21f2e103377" alt="Genre">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/ee3ad92a-00ca-4c52-84dc-2df2d9f4fee7" alt="AdminAuthor">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/a9887a98-3c30-4a7a-9eb3-38c6aaba48f1" alt="User">
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/f446901e-943b-4923-a4ba-1de2caa9810a" alt="Statistics">
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/6ef57ff1-01ca-4a7f-898e-769232072bb3" alt="Statistics2">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/34b720c6-5db1-43ce-96b1-1fcd0732420b" alt="myPost">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/6c16795d-a4f1-4c7e-9f92-93e0a9328ab3" alt="EditArticle">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/51cecec9-9064-4a2a-a50f-6374a75772c2" alt="Articles">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/e028c789-62a8-4303-ae3f-cc1038e665bc" alt="filltredarticle">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/40d52aef-d03a-4ec6-9fe3-dd78d534df15" alt="filteredArticle2">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/acaa40bb-c29a-4e67-97ad-165491e22da6" alt="articileModal">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/4f08474a-b8f2-4374-81bf-f10d069d8f79" alt="authorProfile">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/8a46ab33-3500-4510-a6d7-28cb0032d33b" alt="profile">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/2eb4b854-0d9a-4d18-8209-675e0c9ccb30" alt="indexArticle">
<br/>
<hr/>
<br/>
<img src="https://github.com/ErenColk/BlogProjectOnion/assets/137501644/529c03a3-8328-47db-bbe4-ebd19920fdb6" alt="Comnment">














