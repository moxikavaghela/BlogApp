# 📝 Blogging Application - ASP.NET Core 9 MVC

Welcome to this **Complete ASP.NET Core 9 MVC Course Project**, where we build a fully functional Blogging Application using:

- ASP.NET Core 9 MVC  
- Entity Framework Core (EF Core)  
- Identity for authentication

This project is ideal for beginners and intermediate developers looking to enhance their full-stack web development skills.

---

## 🔗 GitHub Repository

[👉 Source Code on GitHub](https://github.com/moxikavaghela/BlogApp/)

---

## 📌 What You Will Learn

✅ Setting up an ASP.NET Core MVC project  
✅ Using Entity Framework Core (EF Core) for database interactions  
✅ Creating a full Blog CRUD Application  
✅ Designing Models, Controllers, and Views  
✅ Integrating Identity for user registration & login  
✅ Styling with Bootstrap and Razor Pages  
✅ Configuring SQL Server & applying migrations  
✅ Implementing Dependency Injection & Repository Pattern  

---

## 🛠️ Technologies Used

- ASP.NET Core 9 MVC  
- Entity Framework Core  
- Identity  
- SQL Server  
- Razor Pages  
- Bootstrap 5  
- C#  
- Visual Studio  

---

## 🗂️ Project Modules & Relationships

### 🧩 Entity Relationships

- **Category ↔ Post**  
  One-to-Many (1:m)  
  > A single Category can have multiple Blog Posts.

- **Post ↔ Comment**  
  One-to-Many (1:m)  
  > A single Blog Post can have multiple Comments.

### 📦 Module Overview

#### 📁 Category Module  
- Manages blog categories  
- Stores category name and optional description  
- Linked to posts via `CategoryId`

#### 📝 Post Module  
- Handles blog content  
- Stores title, content, author name, image path, published date  
- Belongs to one category  
- Has multiple comments

#### 💬 Comment Module  
- Allows users to add comments to blog posts  
- Stores username, comment content, and date  
- Linked to posts via `PostId`

---

## 🔒 Authentication System

- Built using **ASP.NET Core Identity**  
- Includes features like:
  - User registration & login  
  - Password hashing  
  - Role-based authorization  

---

## 🧪 Features

- Blog Creation, Editing, Deletion  
- Commenting System  
- Category-based filtering  
- Identity-based authentication  
- Responsive UI with Bootstrap  
- Clean architecture using Repository Pattern  

---

## 📬 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 🙌 Credits

Special thanks to [MrIncognito022](https://github.com/MrIncognito022/SyncSyntax) for the original course and inspiration.

---
