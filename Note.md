# Миграции

```
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef migrations list
dotnet ef migrations remove
```

# Генерация контроллеров

```
dotnet aspnet-codegenerator controller -name BlogPostController -m BlogPostModel -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout
```

# Заметки

``` C#
return View(await _context.BlogPosts.Include(u => u.User).ToListAsync());
```

sudo systemctl restart app.service
sudo systemctl restart nginx
dotnet publish -c Release -o /app

109.234.37.141