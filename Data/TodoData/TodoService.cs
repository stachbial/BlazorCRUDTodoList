using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace TodoListSB76764.Data.TodoData
{
    public class TodoService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public TodoService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync(string userName)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.TodoItems.Where(item=>item.OwnerUserName == userName).AsNoTracking().ToListAsync();
        }

        public async Task<TodoItem?> GetTodoItemById(int id, string userName)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.TodoItems.FirstOrDefaultAsync(item => item.Id == id && item.OwnerUserName == userName);
        }

        public async Task<TodoItem?> UpdateTodoItemStatus(int id, bool completeStatus, string userName)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var updatedTodoItem = await context.TodoItems.FirstOrDefaultAsync(item => item.Id == id && item.OwnerUserName == userName);
            if(updatedTodoItem != null && updatedTodoItem.Completed != completeStatus)
            {
                updatedTodoItem.Completed = completeStatus;
                await context.SaveChangesAsync();
                return updatedTodoItem;
            }
            return null;
        }

        public async Task DeleteTodoItemAsync(int id, string userName)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var updatedTodoItem = await context.TodoItems.FirstOrDefaultAsync(item => item.Id == id && item.OwnerUserName == userName);
            if (updatedTodoItem != null)
            {
                context.TodoItems.Remove(updatedTodoItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem item)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.TodoItems.Add(item);
            await context.SaveChangesAsync();
            return item;
        }

    }
}