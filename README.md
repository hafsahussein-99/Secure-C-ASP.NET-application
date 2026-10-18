# Secure User Authentication Portal

A production-ready full-stack user registration module engineered in C# using the ASP.NET Core MVC framework. This codebase serves as a blueprint for implementing structural defensive coding practices against standard application threat vectors.

## Security Engineering Blueprint & Controls
* **SQL Injection Mitigation:** Implements strict Parameterized SQL Command parameters natively (`Microsoft.Data.SqlClient`) ensuring all user entries are typed and isolated from database runtime execution tracks.
* **Cross-Site Scripting (XSS) Defenses:** Employs front-end Razor context-aware contextual HTML encoding filters alongside strong model-state regular expression validation layer structures.
* **CSRF Mitigation:** Configured with robust cryptographically signed Anti-Forgery Token validation processing attributes (`[ValidateAntiForgeryToken]`) across active data modification routes.
* **Cryptographic Layer:** Features secure SHA-256 password processing algorithms to shield sensitive credentials during persistence routines.

## Installation & Environment Details
* Target Framework: .NET Core SDK / ASP.NET Runtime 8.0+
* Development Tools: Visual Studio / VS Code, SQL Server Management Studio (SSMS)
