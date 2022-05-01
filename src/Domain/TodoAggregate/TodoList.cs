using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.TodoAggregate
{
    public class TodoList : DomainBase
    {
        public string Title { get; set; }
        public IList<TodoItem> Items { get; set; }
    }
}