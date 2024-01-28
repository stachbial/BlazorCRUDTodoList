namespace TodoListSB76764.Data.TodoData
{ 
public class TodoItem
{
    public int Id { get; set; }

    public required string OwnerUserName { get; set; }
    public required string Text { get; set; }
    public bool Completed { get; set; }
}}
