using System;
using System.Collections.Generic;

namespace MvcStarterKit.Models
{
    /// <summary>
    /// Default user context 
    /// </summary>
    public class UserContext
    {
        public string Username { get; set; }
        
        public string EmailAddress { get; set; }
        
        public DateTime LoginTime { get; set; }

        public int UserId { get; set; }

        private readonly Dictionary<string, object> _innerItems = new Dictionary<string, object>();

        public void Set(string key, object value)
        {
            _innerItems[key] = value;
        }

        public TResult Get<TResult>(string key) where TResult : class
        {
            return _innerItems[key] as TResult;
        }
    }
}