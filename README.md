# Company Web API



Bu projede şirketlere ait departmanlarda bulunan çalışanların yönetilmesi üzerine bir uygulama geliştirilmiştir. ASP.NET Core ile yazılan bu API ile istenilen şirkete yeni bir çalışan eklenebilir, çalışanların listesi getirilebilir, ilgili çalışanın firması güncellenebilir ve seçilen çalışan silinebilir. Aynı şekilde tüm şirketler ve departmanlar listelenebilir.



## Giriş



> Giriş İşlemi

Oluşturulan API'yi kullanabilmek için yetkinizin olması gerekir. Bunun için veri tabanında bir kaydınızın olması lazım. Örnek olarak 'admin' kullanıcısı ile giriş yapalım. Giriş işleminin başarılı olması durumunda API kullanıcıya JSON Web Token verir. Bu token kullanılarak tüm işlemler gerçekleştirilebilir.

````http
http://localhost:57277/api/login
````

 Yukarıdaki gibi bir istek atıldığında Unauthorized hatası alacaksınız. Bunun nedeni yetkili bir kullanıcı ile giriş yapmamış olmanızdır.

````json
{
    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
    "title": "Unauthorized",
    "status": 401,
    "traceId": "|4db45125-40d22b7aa146078f."
}
````

Kullanıcı adı ve şifre ile giriş yaparak bir JWT elde edebilirsiniz.



````http
http://localhost:57277/api/login?username=admin&password=admin
````

Bu isteğin sonucunda kullanıcıya bir JWT geri dönecektir. Bu tokeni kullanarak diğer işlemleri gerçekleştirebilirsiniz.

````json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOlsiYWRtaW4iLCJhZG1pbiJdLCJqdGkiOiJjNzk4ZjhhOC1lYWFmLTRhZjgtYjFjNy1kN2M5MzhjNTY3ODkiLCJleHAiOjE2MTQ3MTczNDgsImlzcyI6ImVyYWxwc29mdHdhcmUuY29tIiwiYXVkIjoiZXJhbHBzb2Z0d2FyZS5jb20ifQ.fSOlrOABXO_Ox7B4QPY3tTE2QRaBWqbmWp4FSnyrZQo"
}
````



> Çalışanları Listeleme

Firmalarda bulunan tüm çalışanları listelemek için **GET** yöntemi ile ilgili bağlantıya istek atmanız yeterli olacaktır.

````http
http://localhost:57277/api/staff
````

NOT : Çalışan isimleri rastgele ad soyadı oluşturucu tarafından oluşturulmuştur. Hiçbir gerçeklik taşımamaktadır.

Sonuç:

````json
[
    {
        "staffID": 29,
        "staffName": "NİZAR",
        "staffSurname": "KARAÇAM",
        "companyID": 5
    },
    {
        "staffID": 28,
        "staffName": "BANU",
        "staffSurname": "ÇİÇEK",
        "companyID": 4
    },
    {
        "staffID": 27,
        "staffName": "HAKAN",
        "staffSurname": "KARACA",
        "companyID": 5
    }
    ...
]
````



> Çalışan Ekleme

İlgili firmaya yeni bir çalışan eklemek için ilgili bağlantıya çalışana ait bilgileri vererek yeni bir kayıt oluşturabilirsiniz. **POST** yöntemi ile gerçekleştirmelisiniz! İstek sonunda çalışanın güncel bilgileri geriye döner.

````http
http://localhost:57277/api/staff
````

 

> Çalışan Güncelleme

Bir çalışanın firmasını güncellemek istersek o kullanıcıya ait ID numarasını ve şirketin ID numarasını bilmemiz gerekecektir. **PUT** yöntemi ile aşağıdaki gibi bir istek atıldığı zaman ilgili çalışanın firması güncellenmiş olacaktır. İlk parametre çalışan ID iken diğer parametre ise şirketin ID bilgisidir. İstek sonunda çalışanın güncel bilgileri geriye döner.

````http
http://localhost:57277/api/staff/30/3
````



> Çalışan Silme

İstenilen çalışan veri tabanından silinmek istenirse kullanıcıya ait ID parametresi bağlantı üzerinde belirtilmelidir. **DELETE** yöntemi ile gerçekleştirilen istek sonucunda ilgili çalışan silinmiş olur. Silinen çalışanın bilgileri geriye döner.

````http
http://localhost:57277/api/staff/30
````



> Firmaları Listeleme

Veri tabanında bulunan tüm firmaları ve bu firmalara ait kaç adet çalışanın olduğunu **GET** yöntemi ile ilgili bağlantıya istek atarak görebiliriz.

````http
http://localhost:57277/api/companies
````



> Departmanları Listeleme

Veri tabanında bulunan departmanları GET yöntemi ile ilgili bağlantıya istek göndererek görebilirsiniz. 

````http
http://localhost:57277/api/department
````



> Çalışanları Departmanlara Göre Listeleme

Her departmana ait çalışanları çalıştığı firmalarla birlikte görebileceğiniz  istekte bulunabilirsiniz. Bunun için **POST** yöntemi ile ilgili bağlantıya istek göndermeniz yeterli olacaktır.

````http
http://localhost:57277/api/department
````



## Notlar

* Veri tabanını dosyasını repo üzerinden indirip veri tabanınıza entegre edebilirisiniz.
* Veri tabanı bağlantı dizgisini appsettings.json dosyası üzerinden değiştirebilirsiniz.
* Veri tabanı olarak MSSQL kullanılmıştır.
* Veri tabanında bulunan veriler hiçbir şekilde gerçeği yansıtmamaktadır.



## Yazarlar

Emirhan KIRAN - [Emirhan KIRAN](https://www.linkedin.com/in/emir-kiran/)
