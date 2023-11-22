using System;
using System.ComponentModel.DataAnnotations;

public class Todo
{
    [Key]
    public int Id { get; set; }

    public string? Task { get; set; }

    public bool Completed { get; set; }

    public DateTime Date { get; set; }

    public int? TodoListId { get; set; }
    public TodoList TodoList { get; set; }
}

public class TodoList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Todo> Todos { get; set; }
}
