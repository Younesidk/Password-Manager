# 🔐 C# Password Manager (Beginner Project)

This is a simple **console-based password manager** written in C#. I built this as my **first real C# project**, It helped me understand how to work with functions, JSON, file I/O, and user input.

---

## ✨ Features

- 🔑 **Password Generator**
  - Customizable: choose to include uppercase, lowercase, digits, and special characters.
  - Randomized and length-controlled output.

- 🧠 **Password Strength Checker**
  - Checks for:  
    ✅ Length (8+ characters)  
    ✅ Uppercase  
    ✅ Lowercase  
    ✅ Digit  
    ✅ Special character

- 💾 **Save New Passwords**
  - Save platform name, email/username, and password to a local `.json` file.

- 📜 **Show All Saved Passwords**
  - Lists all stored credentials with simple formatting.

---

## 🛠 How It Works

You'll be greeted with this main menu:

PASSWORD MANAGER

1. Generate password

2. Test the strength of a password

3. Save New Password

4. Show All Saved Passwords

5. Exit

Each feature guides you interactively through the steps.

---

## 📁 Data Storage

Passwords are stored in a readable JSON file at:

C:\Study\C# learning\Password Manager C#\vault.json

> ⚠️ **Disclaimer**: This project does **not use encryption** and is intended for **educational purposes only**. Do not store real/secure credentials.

---

## 💡 What I Learned

- Using `Func<>` arrays to store and call multiple function types.
- Validating user input.
- Random password generation based on criteria.
- Working with JSON via `System.Text.Json`.
- Basic file handling in C#.

---

## ✅ Built With

- `C#` (.NET Core)
- `System.Text.Json`
- `StringBuilder`
- `Func<>` delegates and lambdas

---

## 📚 License

This project is open-source and free to use for learning or adaptation.
