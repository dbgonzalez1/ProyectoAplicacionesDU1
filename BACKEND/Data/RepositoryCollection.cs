using System.Collections;
using System.ComponentModel.DataAnnotations;
using backend.Exceptions;
using backend.Models;

namespace backend.Data;

public class RepositoryCollection<T> : IEnumerable<T> where T : HasEntity
{
    private readonly List<T> _items = [];
    private readonly object _lock = new();
    
    public Task AddAsync(T item)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                item.Id = _items.Count + 1;
                Validate(item);
                _items.Add(item);
            }
        });
    }
    
    public Task UpdateAsync(T item)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                var index = _items.FindIndex(el => el.Id == item.Id);
                if (index == -1)
                {
                    throw new ErrorDeCliente("No se encontró el elemento");
                }
                Validate(item);
                _items[index] = item;
            }
        });
    }
    
    public Task<T?> FindAsync(int id)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                return _items.Find(item => item.Id == id);
            }
        });
    }
    
    public Task<bool> AnyAsync(Func<T, bool> predicate)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                return _items.Any(predicate);
            }
        });
    }
    
    public Task<List<T>> ToListAsync()
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                return _items.ToList();
            }
        });
    }
    
    public Task RemoveAsync(int id)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                var index = _items.FindIndex(el => el.Id == id);
                if (index == -1)
                {
                    throw new ErrorDeCliente("No se encontró el elemento");
                }
                _items.RemoveAt(index);
            }
        });
    }
    
    public Task RemoveAsync(T item)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                var index = _items.FindIndex(el => el.Id == item.Id);
                if (index == -1)
                {
                    throw new ErrorDeCliente("No se encontró el elemento");
                }
                _items.RemoveAt(index);
            }
        });
    }


    private static void Validate(T entity)
    {
        Console.WriteLine("Try validate");

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(entity, serviceProvider: null, items: null);

        var errors = new List<string>();
            
        if (Validator.TryValidateObject(entity, validationContext, validationResults, true))
        {
            return;
        }

        foreach (var validationResult in validationResults)
        {
            if (validationResult.ErrorMessage != null)
            {
                errors.Add(validationResult.ErrorMessage);
            }
        }

        throw new ErrorDeValidacionDeAtrr(errors);
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}