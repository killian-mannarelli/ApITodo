using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(TodoContext context)
    {
        if (!context.Todo.Any())
        {
            context.Todo.AddRange(
                new Todo
                {
                    Task = "Learn Entity Framework",
                    Completed = false,
                    Date = DateTime.Now.AddDays(7) // une semaine plus tard
                },
                new Todo
                {
                    Task = "Complete project report",
                    Completed = true,
                    Date = DateTime.Now.AddDays(-3) // trois jours dans le passé
                },
                new Todo
                {
                    Task = "Go to the grocery store",
                    Completed = false,
                    Date = DateTime.Now // aujourd'hui
                }
            );

            context.SaveChanges();
        }
    }
}
