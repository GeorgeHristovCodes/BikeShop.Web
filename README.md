# 🚴 BikeShop.Web

ASP.NET MVC уеб приложение за онлайн магазин и наем на велосипеди и аксесоари. Проектът е част от дипломната работа на Георги Тодоров Христов, ученик от 12Б клас, с ръководител Иван Иванов.

## 🛠️ Използвани технологии

- ASP.NET Core 7.0 MVC
- Entity Framework Core (Code First)
- MS SQL Server
- Razor Views
- Bootstrap 5
- Identity (с роли Admin и User)
- Toastr.js (за pop-up съобщения)
- Render (за деплой)

## 🎯 Основни функционалности

### 🛒 Магазин
- Покупка на велосипеди по категории: Шосейни, Планински (твърда рамка и с окачване), Градски, Детски
- Филтриране по:
  - Категория
  - Бранд (DRAG, NS BIKES, SPECIALIZED, YT INDUSTRIES)
  - Размер на рамката (XS–XXL)
  - Ценови диапазон
- Кошница с поддръжка за гости и логнати потребители
- Комбинирана поръчка – възможност за покупка и наем едновременно

### 🚴 Наем на велосипеди
- Избор на дати
- Калкулация на крайна цена
- Попълване на данни: Име, Фамилия, Телефон, Град

### 🎒 Аксесоари
- Категории: Каски, Ръкавици, Помпи, Инструменти, Наколенки, Лакътници, Очила
- Добавяне в кошницата и поръчка
- Отделна страница и страничен панел с филтри

### 👤 Потребителски профил
- Преглед на направени поръчки (покупки, наеми, аксесоари)
- Детайли на всяка поръчка

### 🔐 Админ панел
- Управление на велосипеди и аксесоари (CRUD)
- Управление на потребители и роли
- Преглед на всички поръчки и наеми

## 🧪 Тестване и сигурност
- Unit тестове с над 65% покритие
- Оптимизация на скоростта и защитата
- Кросбраузър съвместимост

## 📸 Начална страница
- Модерен дизайн с Bootstrap
- Секции за препоръчани велосипеди, категории и оферти
- Слайдшоу за представяне на продуктите

## 🎯 Screenshots
![image](https://github.com/user-attachments/assets/0c0b82d0-2835-44a6-a12b-0dd5de21bca1)
![image](https://github.com/user-attachments/assets/7f230755-7ca2-4747-a67b-2121f1fb4193)
![image](https://github.com/user-attachments/assets/5b20a630-2bf7-404b-a61a-e08bf68a0397)
![image](https://github.com/user-attachments/assets/9a14d784-c5fa-4dec-be8c-05f78eca1d95)
![image](https://github.com/user-attachments/assets/84ad1903-e8e3-4242-88cd-3c0449c2d39b)
![image](https://github.com/user-attachments/assets/3fe8a3e2-5322-436c-b95c-f018257a3280)
![image](https://github.com/user-attachments/assets/8267ff35-be49-4fec-a79d-a60d6a5fa1f4)
![image](https://github.com/user-attachments/assets/b00843a7-fb68-46b8-9592-f79a86eeaccc)
![image](https://github.com/user-attachments/assets/10ecf657-8ec6-4be2-9945-1683c153055f)
![image](https://github.com/user-attachments/assets/4a4f11ce-e2b9-43b3-a09f-dbecd948d561)
![image](https://github.com/user-attachments/assets/5ac52a78-7bad-4929-8146-f20ab9eb584a)
![image](https://github.com/user-attachments/assets/1c625368-5b8d-41d7-a2ab-9244bf99290f)
![image](https://github.com/user-attachments/assets/8ee83491-c6ea-49d9-8868-b4df9d95bba6)
![image](https://github.com/user-attachments/assets/b604a1dd-73d4-4969-9b31-b7eb80cf6e20)
![image](https://github.com/user-attachments/assets/dd9e6f28-3c31-45e6-8817-2b7bb0677092)
![image](https://github.com/user-attachments/assets/eae88378-92f1-4f04-a27f-cfc302867877)
![image](https://github.com/user-attachments/assets/fea4f3c8-fd57-4907-9bf8-7f8e1ba12ba7)
![image](https://github.com/user-attachments/assets/4e456e91-2e07-4ec5-94d3-bfc1ebe4c38d)

---

## 📁 Инсталация

1. Клонирай репозиторито:

```bash
git clone https://github.com/GeorgeHristovCodes/BikeShop.Web.git
трябва да направите миграция на базата данни за успешно включване
акаунт за админ: admin@bikeshop.com  Admin123!
акаунт за стаф: staff@bikeshop.com   Staff123!
акаунт за потребител:  user@bikeshop.com  User123!
