using System;
//1
class PhoneBook 
{
    private Dictionary<string, string> contacts = new Dictionary<string, string>();
   
     public string this[string phoneNum]
    {
        get
        {
            if (contacts.ContainsKey(phoneNum))
                return contacts[phoneNum];
            else
                return "Phone Number not found";
        }
        set
        {
            contacts[phoneNum] = value;
        }}}
 
class Program{

    static void Main()
    {
        PhoneBook myContacts = new PhoneBook();
        
        myContacts["Peter"] = "0101234567";
        myContacts["Emma"] = "0109876543";
        
        Console.WriteLine(myContacts["Emma"]);
        
    }
}
//---------------------------------------------------------------
//2 Build a WeeklySchedule class where you can access daily schedules using day names: schedule["Monday"].///

// class  WeeklySchedule 
// {
//     private Dictionary<string, string> schedules = new Dictionary<string, string>();
   
//      public string this[string day]
//     {
//         get
//         {
//             if (schedules.ContainsKey(day))
//                 return schedules[day];
//             else
//                 return "this day does not have a schedules";
//         }
//         set
//         {
//             schedules[day] = value;
//         }
//     }
// }
// class Program
// {
//     static void Main()
//     {
// WeeklySchedule mySchedules =new WeeklySchedule();

// mySchedules["Sunday"] = "English Course";
// mySchedules["Monday"] = "Frontend Course";
// mySchedules["Tuesday"] = "Network Course";
// Console.WriteLine(mySchedules["Monday"]);
//   }}
//-----------------------------------------------------------------------
// //4.
// class Stack<T>
// {
//  private List<T> items = new List<T>();
//  public int Count
//     {
//         get { return items.Count; }
//     }

//     public void Push(T item)
//     {
//         items.Add(item);
//     }

//     public T Pop()
//     {
//         if (items.Count == 0)
//             throw new InvalidOperationException("Stack is empty! Cannot pop.");

//         int lastIndex = items.Count - 1;
//         T value = items[lastIndex];
//         items.RemoveAt(lastIndex);
//         return value;
//     }

//     public T Peek()
//     {
//         if (items.Count == 0)
//             throw new InvalidOperationException("Stack is empty! Cannot peek.");

//         return items[items.Count - 1];
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//   Stack<string> items = new Stack<string>();
// items.Push("pinterest");
// items.Push("shein");
// items.Push("git");

//    string CurrentPage = items.Pop();
//  string PreviousPage = items.Peek();
// Console.WriteLine ("current Page " +CurrentPage ); 
// Console.WriteLine ("Previous Page "+PreviousPage ); 
//  }}
//----------------------------------------------------------------------------
// //5
// class Quiz
// {
//  private Dictionary<string,int> grades = new Dictionary<string,int> ();
// public Quiz(){
// grades.Add("Emma", 87);
// grades.Add("Andrew",92);
// grades.Add("Ziad", 95);

// }
//  public void GetGrade(string name)
//     {
//         if (grades.TryGetValue(name, out int grade))
//         {
//             Console.WriteLine($"{name}'s grade: {grade}");
//         }
//         else
//         {
//             Console.WriteLine($"{name} not found!");
//         }
//     }}

// class Program
// { static void Main(string[] args)
//     {
//      Quiz quiz = new Quiz();
       
//         quiz.GetGrade("Emma");
//         quiz.GetGrade("Andrew");
//         quiz.GetGrade("Ziad"); }}
 //---------------------------------------------------------------------
//6
// class Cache<TKey, TValue> where TKey : notnull
// {
//     private Dictionary<TKey, CacheItem> _items;

//     private class CacheItem
//     {
//         public TValue? Value { get; set; }
//         public DateTime ExpirationTime { get; set; }
//     }

//     public Cache()
//     {
//         _items = new Dictionary<TKey, CacheItem>();
//     }

//     public void Add(TKey key, TValue value, TimeSpan duration)
//     {
//         var item = new CacheItem
//         {
//             Value = value,
//             ExpirationTime = DateTime.Now.Add(duration)
//         };

//         _items[key] = item;
//         Console.WriteLine($"Added: {key} (expires in {duration.TotalSeconds} seconds)");
//     }

//     public bool TryGet(TKey key, out TValue? value)
//     {
//         value = default;

//         if (!_items.ContainsKey(key))
//             return false;

//         var item = _items[key];

//         if (DateTime.Now > item.ExpirationTime)
//         {
//             _items.Remove(key);
//             return false;
//         }

//         value = item.Value;
//         return true;
//     }

//     public TValue Get(TKey key)
//     {
//         if (TryGet(key, out TValue? value))
//         {
//             return value!;
//         }

//         throw new KeyNotFoundException($"Key '{key}' not found or expired");
//     }
// }
// class Program
// {
//     static void Main(string[] args)
//     {
//         var cache = new Cache<string, string>();

//         cache.Add("user1", "CEO", TimeSpan.FromSeconds(5));
//         cache.Add("user2", "Manager", TimeSpan.FromSeconds(10));

//         if (cache.TryGet("user1", out var result1))
//             Console.WriteLine($"user1: {result1}");
//         else
//             Console.WriteLine("Expired or not found");}}
//----------------------------------------------------------------------------------------
//7
// class Program
// {
//     static void Main()
//     {
//         List<int> numbers = new List<int> { 1, 2, 3, 4 };

//         List<string> result = ConvertList<int, string>(
//             numbers,
//             n => n.ToString()
//         );

//         foreach (var item in result)
//         {
//             Console.WriteLine(item);
//         }
//     }

//     public static List<TTarget> ConvertList<TSource, TTarget>(
//         List<TSource> source,
//         Func<TSource, TTarget> converter)
//     {
//         List<TTarget> result = new List<TTarget>();

//         foreach (var item in source)
//         {
//             result.Add(converter(item));
//         }

//         return result;
//     }
// }
//-------------------------------------------------------------------------------
//8
// public interface IEntity
// {
//     int Id { get; set; }
// }
// public class User : IEntity
// {
//     public int Id { get; set; }
//     public string Name { get; set; } = "";
// }
// public class Repository<T> where T : IEntity
// {
//     private List<T> _items = new List<T>();

//     public void Add(T item)
//     {
//         _items.Add(item);
//         Console.WriteLine($"Added item with Id {item.Id}");
//     }
//     public T? GetById(int id)
//     {
//         return _items.FirstOrDefault(x => x.Id == id);
//     }

//     // Update
//     public void Update(T item)
//     {
//         var existing = GetById(item.Id);
//         if (existing != null)
//         {
//             int index = _items.IndexOf(existing);
//             _items[index] = item;
//             Console.WriteLine($"Updated item with Id {item.Id}");
//         }
//         else
//         {
//             Console.WriteLine($"Item with Id {item.Id} not found!");
//         }
//     }

//     // Delete
//     public void Delete(int id)
//     {
//         var existing = GetById(id);
//         if (existing != null)
//         {
//             _items.Remove(existing);
//             Console.WriteLine($"Deleted item with Id {id}");
//         }
//         else
//         {
//             Console.WriteLine($"Item with Id {id} not found!");
//         }
//     }
// }
// class Program
// {
//     static void Main()
//     {
//         var userRepo = new Repository<User>();

//         userRepo.Add(new User { Id = 1, Name = "mody" });
//         userRepo.Add(new User { Id = 2, Name = "lolo" });

       

//         // Update
//         userRepo.Update(new User { Id = 2, Name = "Robert" });

//         // Read by Id
//         var user2 = userRepo.GetById(2);
//         Console.WriteLine($"\nUser with Id 2: {user2?.Name}");

//         // Delete
//         userRepo.Delete(1);

//     }
// }

//--------------------------------------------------------------------------------------------
//9contact manager 

// class ContactManager
// {
//     private Dictionary<string, string> contacts;
//     public ContactManager(){
//          contacts = new Dictionary<string, string>();
//     }
//    public void AddContact(string name, string phone)
//     {
//         if (contacts.ContainsKey(name))
//         {
           
//        Console.WriteLine("this name already exist");
//         }
//         else
//         {
//             contacts.Add(name,phone);
//              Console.WriteLine($"contacts '{name}' added successfully");
//         }
//     }
//     public void RemoveContact(string name)
//     {
//    if (contacts.Remove(name))
//     {
//         Console.WriteLine($"Contact {name} removed successfully!");
//     }
//     else
//     {
//         Console.WriteLine($"Contact {name} not found!");
//     }
// }
//   public void SearchContact(string name)
//     {
//     if (contacts.TryGetValue(name,out string phone)) 
//     {
//         Console.WriteLine($"Name {name},Phone {phone} ");
//     }
//     else
//     {
//         Console.WriteLine($"Contact {name} not found!");
//     }}}

// class Program
// {
//     static void Main()
//     {
//         ContactManager manager =new ContactManager();
//           manager.AddContact("mody", "0101234567");
//         manager.AddContact("fify", "0109876543");
        
//         manager.SearchContact("mody");
//         manager.RemoveContact("fify");
//         manager.SearchContact("fify");
//     }
// }
//---------------------------------------------------------------------------------
//10
// public class Product
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public decimal Price { get; set; }

//     public Product(int id, string name, decimal price)
//     {
//         Id = id;
//         Name = name;
//         Price = price;
//     }
// }

// public class ShoppingCart
// {
//     private List<Product> items = new List<Product>();
//     private Dictionary<int, int> quantities = new Dictionary<int, int>();
//     private HashSet<string> discounts = new HashSet<string>();

//     public void AddProduct(Product product, int quantity)
//     {
//         items.Add(product);

//         if (quantities.ContainsKey(product.Id))
//             quantities[product.Id] += quantity;
//         else
//             quantities[product.Id] = quantity;
//     }

//     public void AddDiscount(string discountCode)
//     {
//         discounts.Add(discountCode);
//     }

//     public decimal GetTotal()
//     {
//         decimal total = 0;

//         foreach (var product in items)
//         {
//             int quantity = quantities[product.Id];
//             total += product.Price * quantity;
//         }

//         if (discounts.Contains("SAVE10"))
//             total *= 0.9m;

//         return total;
//     }

//     public void ShowCart()
//     {

//         foreach (var product in items)
//         {
//             int quantity = quantities[product.Id];
//             Console.WriteLine($"{product.Name} = ${product.Price} x {quantity} = ${product.Price * quantity}");
//         }
//         Console.WriteLine($"Total: ${GetTotal()}");
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         ShoppingCart cart = new ShoppingCart();

//         Product p1 = new Product(1, "Phone", 10000);
//         Product p2 = new Product(2, "Headset", 500);

//         cart.AddProduct(p1, 1);
//         cart.AddProduct(p2, 2);

//         cart.AddDiscount("SAVE10");

//         cart.ShowCart();
//     }}
//------------------------------------------------------------------------------------
//11
// public class Calculator
// {
// public static double? CalculateAverage(int?[] numbers)
// {
//     int sum = 0;
//     int count = 0;

//     foreach (int? num in numbers)
//     {
//         if (num.HasValue)   {
//             sum += num.Value;
//             count++;
//         }
//     }

//     if (count == 0)
//         return null;  

//     return (double)sum / count;
// }}
// class Program
// {
//     static void Main()
//     {
//         int?[] numbers = { 10, 20, null, 30, null };

//         double? avg = Calculator.CalculateAverage(numbers);

//         if (avg.HasValue)
//             Console.WriteLine("Average = " + avg.Value);
//         else
//             Console.WriteLine("No valid numbers to calculate average.");
//         } }
//----------------------------------------------------------------
//12
// public class Person{
//     public string FirstName { get; set; }
//     public string? MiddleName { get; set; }   
//     public string LastName { get; set; }
//     public DateTime? DateOfBirth { get; set; } 

//     public override string ToString() {
//         string fullName = MiddleName == null
//             ? $"{FirstName} {LastName}"
//             : $"{FirstName} {MiddleName} {LastName}";

//         string dob = DateOfBirth?.ToShortDateString() ?? "Not Provided";

//         return $"Name: {fullName}, DOB: {dob}";
//     }}
// class Program{
//     static void Main(){
//         Person p1 = new Person
//         {
//             FirstName = "Margret",
//             LastName = "Emil",
//             DateOfBirth = new DateTime(2003, 5, 10)};
        
//         Console.WriteLine(p1);}}
//-----------------------------------------------------------------
//13
// public static class IntExtensions
// {
//     public static bool IsEven(this int n) => n % 2 == 0;

//     public static bool IsOdd(this int n) => n % 2 != 0;

//     public static bool IsPrime(this int n)
//     {
//         if (n <= 1) return false;
//         for (int i = 2; i <= Math.Sqrt(n); i++)
//             if (n % i == 0) return false;
//         return true;
//     }

//     public static long Factorial(this int n)
//     {
//         long result = 1;
//         for (int i = 2; i <= n; i++)
//             result *= i;
//         return result;
//     }

//     public static string ToRoman(this int n)
//     {
//         string[] r = { "M","CM","D","CD" };
//         int[] v =     {1000,900,500,400};

//         string result = "";
//         for (int i = 0; i < v.Length; i++)
//             while (n >= v[i]) { result += r[i]; n -= v[i]; }

//         return result; }}
// class Program{
//  static void Main() {

//         int x = 7;
//         Console.WriteLine(x.IsEven());
//         Console.WriteLine(x.IsPrime());
//         Console.WriteLine(5.Factorial());
//         Console.WriteLine(500.ToRoman());
//    }}
//------------------------------------------------------------------
//15
// public static class CollectionExtensions
// {
//     public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
//     {
//         if (batchSize <= 0)
//             throw new ArgumentException("Batch size must be greater than 0");

//         List<T> batch = new List<T>(batchSize);
        
//         foreach (var item in source)
//         {
//             batch.Add(item);
//             if (batch.Count == batchSize)
//             {
//                 yield return batch;
//                 batch = new List<T>(batchSize);
//             }
//         }
        
//         if (batch.Count > 0)
//             yield return batch;
//     }

//     public static (List<T> matching, List<T> notMatching) Partition<T>(
//         this IEnumerable<T> source, Func<T, bool> predicate)
//     {
//         var matching = new List<T>();
//         var notMatching = new List<T>();

//         foreach (var item in source)
//         {
//             if (predicate(item))
//                 matching.Add(item);
//             else
//                 notMatching.Add(item);
//         }

//         return (matching, notMatching);
//     }

//     public static IEnumerable<T> FindDuplicates<T>(this IEnumerable<T> source)
//     {
//         var seen = new HashSet<T>();
//         var duplicates = new HashSet<T>();

//         foreach (var item in source)
//         {
//             if (!seen.Add(item))
//                 duplicates.Add(item);
//         }

//         return duplicates;
//     }

//     public static Dictionary<T, int> FindDuplicatesWithCount<T>(this IEnumerable<T> source) where T : notnull
//     {
//         return source.GroupBy(x => x)
//                     .Where(g => g.Count() > 1)
//                     .ToDictionary(g => g.Key, g => g.Count());
//     }

//     public static Statistics GetStatistics(this IEnumerable<int> source)
//     {
//         var list = source.ToList();
//         if (list.Count == 0)
//             return new Statistics();

//         list.Sort();
        
//         return new Statistics
//         {
//             Count = list.Count,
//             Sum = list.Sum(),
//             Average = list.Average(),
//             Min = list.Min(),
//             Max = list.Max(),
//             Median = GetMedian(list),
//             Mode = GetMode(list)
//         };
//     }

//     public static Statistics GetStatistics(this IEnumerable<double> source)
//     {
//         var list = source.ToList();
//         if (list.Count == 0)
//             return new Statistics();

//         list.Sort();
        
//         return new Statistics
//         {
//             Count = list.Count,
//             Sum = list.Sum(),
//             Average = list.Average(),
//             Min = list.Min(),
//             Max = list.Max(),
//             Median = GetMedian(list),
//             Mode = GetMode(list)
//         };
//     }

//     private static double GetMedian(List<int> sorted)
//     {
//         int count = sorted.Count;
//         if (count % 2 == 0)
//             return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
//         return sorted[count / 2];
//     }

//     private static double GetMedian(List<double> sorted)
//     {
//         int count = sorted.Count;
//         if (count % 2 == 0)
//             return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
//         return sorted[count / 2];
//     }

//     private static double GetMode<T>(List<T> source)
//     {
//         var grouped = source.GroupBy(x => x)
//                            .OrderByDescending(g => g.Count())
//                            .FirstOrDefault();
        
//         return grouped != null ? Convert.ToDouble(grouped.Key) : 0;
//     }

//     public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
//     {
//         foreach (var item in source)
//         {
//             if (item != null)
//                 yield return item;
//         }
//     }

//     public static T? SafeElementAt<T>(this IEnumerable<T> source, int index)
//     {
//         return source.ElementAtOrDefault(index);
//     }

//     public static T? SafeFirst<T>(this IEnumerable<T> source, Func<T, bool>? predicate = null)
//     {
//         return predicate == null 
//             ? source.FirstOrDefault() 
//             : source.FirstOrDefault(predicate);
//     }

//     public static T? SafeLast<T>(this IEnumerable<T> source, Func<T, bool>? predicate = null)
//     {
//         return predicate == null 
//             ? source.LastOrDefault() 
//             : source.LastOrDefault(predicate);
//     }

//     public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, int size)
//     {
//         return source.Batch(size).Select(batch => batch.ToArray());
//     }

//     public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
//     {
//         var seen = new HashSet<TKey>();
//         foreach (var item in source)
//         {
//             if (seen.Add(keySelector(item)))
//                 yield return item;
//         }
//     }

//     public static void SafeForEach<T>(this IEnumerable<T> source, Action<T> action)
//     {
//         foreach (var item in source)
//         {
//             try
//             {
//                 action(item);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error processing item: {ex.Message}");
//             }
//         }
//     }


// public class Statistics
// {
//     public int Count { get; set; }
//     public double Sum { get; set; }
//     public double Average { get; set; }
//     public double Min { get; set; }
//     public double Max { get; set; }
//     public double Median { get; set; }
//     public double Mode { get; set; }

//     public override string ToString()
//     {
//         return $"Count: {Count}, Sum: {Sum:F2}, Avg: {Average:F2}, " +
//                $"Min: {Min:F2}, Max: {Max:F2}, Median: {Median:F2}, Mode: {Mode:F2}";
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("=== BATCH ===");
//         var numbers = Enumerable.Range(1, 10);
//         foreach (var batch in numbers.Batch(3))
//         {
//             Console.WriteLine($"Batch: [{string.Join(", ", batch)}]");
//         }

//         Console.WriteLine("\n=== PARTITION ===");
//         var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//         var (evens, odds) = values.Partition(x => x % 2 == 0);
//         Console.WriteLine($"Evens: [{string.Join(", ", evens)}]");
//         Console.WriteLine($"Odds: [{string.Join(", ", odds)}]");

//         Console.WriteLine("\n=== FIND DUPLICATES ===");
//         var items = new[] { 1, 2, 3, 2, 4, 5, 3, 6, 7, 3 };
//         var duplicates = items.FindDuplicates();
//         Console.WriteLine($"Duplicates: [{string.Join(", ", duplicates)}]");
        
//         var duplicatesWithCount = items.FindDuplicatesWithCount();
//         foreach (var kvp in duplicatesWithCount)
//         {
//             Console.WriteLine($"Value {kvp.Key} appears {kvp.Count} times");
//         }

//         Console.WriteLine("\n=== STATISTICS ===");
//         var data = new[] { 10, 20, 30, 20, 40, 50, 20, 60 };
//         var stats = data.GetStatistics();
//         Console.WriteLine(stats);

        
//         Console.WriteLine("\n=== SAFE ENUMERATION ===");
//         string?[] nullableStrings = { "Hello", null, "World", null, "C#" };
//         var nonNullStrings = nullableStrings.WhereNotNull();
//         Console.WriteLine($"Non-null strings: [{string.Join(", ", nonNullStrings)}]");

//         var safeFirst = values.SafeFirst(x => x > 5);
//         Console.WriteLine($"Safe First > 5: {safeFirst}");

//         var safeLast = values.SafeLast(x => x < 3);
//         Console.WriteLine($"Safe Last < 3: {safeLast}");

//     }
// }}
//------------------------------------------------------
//16
// delegate double MathOperation(double a, double b);

// class Calculator
// {
//     public static double Add(double a, double b) => a + b;
//     public static double Subtract(double a, double b) => a - b;
//     public static double Multiply(double a, double b) => a * b;
//     public static double Divide(double a, double b) => b != 0 ? a / b : throw new DivideByZeroException();

//     public static double Execute(double x, double y, MathOperation operation)
//     {
//         return operation(x, y);
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         double a = 10;
//         double b = 5;

//         Console.WriteLine($"Add: {Calculator.Execute(a, b, Calculator.Add)}");
//         Console.WriteLine($"Subtract: {Calculator.Execute(a, b, Calculator.Subtract)}");
//         Console.WriteLine($"Multiply: {Calculator.Execute(a, b, Calculator.Multiply)}");
//         Console.WriteLine($"Divide: {Calculator.Execute(a, b, Calculator.Divide)}");
//     }
// }
//-----------------------------------------------------------------------------------------------
//17
// delegate void Notify(string message);

// class NotificationSystem
// {
//     public static void EmailNotification(string message)
//     {
//         Console.WriteLine($"Email: {message}");
//     }

//     public static void SMSNotification(string message)
//     {
//         Console.WriteLine($"SMS: {message}");
//     }

//     public static void PushNotification(string message)
//     {
//         Console.WriteLine($"Push: {message}");
//     }

//     public static void SendNotification(string message, Notify notifyChannels)
//     {
//         notifyChannels?.Invoke(message); 
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         Notify allChannels = NotificationSystem.EmailNotification;
//         allChannels += NotificationSystem.SMSNotification;
//         allChannels += NotificationSystem.PushNotification;

        
//         NotificationSystem.SendNotification("Your order has been shipped!", allChannels);

        
//         allChannels -= NotificationSystem.SMSNotification;
//         NotificationSystem.SendNotification("Your payment was successful!", allChannels);
//     }
// }
//-------------------------------------------------------------------
//18
// delegate bool BusinessRule<T>(T data);

// class PluginSystem<T>
// {
//     private readonly List<BusinessRule<T>> rules = new List<BusinessRule<T>>();

//     public void RegisterRule(BusinessRule<T> rule)
//     {
//         rules.Add(rule);
//     }

//     public bool ExecuteAll(T data)
//     {
//         bool allPassed = true;

//         foreach (var rule in rules)
//         {
//             bool result = rule(data);
//             Console.WriteLine($"Rule {rule.Method.Name}: {(result ? "Passed" : "Failed")}");
//             if (!result)
//                 allPassed = false;
//         }

//         return allPassed;
//     }}
//     class Order
// {
//     public string CustomerName { get; set; } = "";
//     public int Quantity { get; set; }
//     public double Price { get; set; }
// }


// class Program
// {
//     static void Main()
//     {
//         var orderSystem = new PluginSystem<Order>();

//         orderSystem.RegisterRule(CheckQuantity);
//         orderSystem.RegisterRule(CheckPrice);
//         orderSystem.RegisterRule(o => !string.IsNullOrEmpty(o.CustomerName)); 
//          var order2 = new Order { CustomerName = "", Quantity = 0, Price = -50 };
//         Console.WriteLine($"\nOrder2 Valid: {orderSystem.ExecuteAll(order2)}");
//     }

//     static bool CheckQuantity(Order order) => order.Quantity > 0;
//     static bool CheckPrice(Order order) => order.Price > 0;
//     }
//19------------------------------------------------------------
// delegate T Transform<T>(T input);
// delegate bool Filter<T>(T input);


// class DataPipeline<T>
// {
//     private readonly List<Transform<T>> transformations = new();
//     private readonly List<Filter<T>> filters = new();

   
//     public void AddTransformation(Transform<T> transform) => transformations.Add(transform);

 
//     public void AddFilter(Filter<T> filter) => filters.Add(filter);

//     public IEnumerable<T> Process(IEnumerable<T> data)
//     {
//         foreach (var item in data)
//         {
//             T current = item;

            
//             foreach (var t in transformations)
//                 current = t(current);

//             bool passed = filters.All(f => f(current));
//             if (passed)
//                 yield return current;
//         }
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//         var pipeline = new DataPipeline<int>();

      
//         pipeline.AddTransformation(n => n * 2);

    
//         pipeline.AddTransformation(n => n - 1);

//         pipeline.AddFilter(n => n % 2 != 0);

        
//         var result = pipeline.Process(numbers);

//         Console.WriteLine("Processed Numbers: " + string.Join(", ", result));
//     }
// }
//---------------------------------------------------------------------------
//20
// class Student
// {
//     public string Name { get; set; } = "";
//     public double Grade { get; set; }
// }

// class Program
// {
//     static void Main()
//     {
//         var students = new List<Student>
//         {
//             new Student { Name = "Alice", Grade = 85 },
//             new Student { Name = "Bob", Grade = 72 },
//             new Student { Name = "Charlie", Grade = 90 },
//             new Student { Name = "David", Grade = 65 },
//             new Student { Name = "Eva", Grade = 78 }
//         };

//         var passed = students.Where(s => s.Grade >= 75);

       
//         var boosted = passed.Select(s => new Student { Name = s.Name, Grade = s.Grade + 5 });

//         double average = boosted.Average(s => s.Grade);

//         Console.WriteLine("Passed Students (with boost):");
//         foreach (var s in boosted)
//             Console.WriteLine($"{s.Name}: {s.Grade}");

//         Console.WriteLine($"\nAverage Grade (boosted): {average:F2}");
//     }
// }
//------------------------------------------------------------------
//21
// public class Rule<T>
// {
//     public Func<T, bool> Test { get; }
//     public string ErrorMessage { get; }

//     public Rule(Func<T, bool> test, string errorMessage)
//     {
//         Test = test;
//         ErrorMessage = errorMessage;
//     }
// }

// public class Validator<T>
// {
//     private List<Rule<T>> _rules = new List<Rule<T>>();

//     public void AddRule(Func<T, bool> test, string errorMessage)
//     {
//         _rules.Add(new Rule<T>(test, errorMessage));
//     }

//     public List<string> Validate(T value)
//     {
//         var errors = new List<string>();

//         foreach (var rule in _rules)
//         {
//             if (!rule.Test(value))
//             {
//                 errors.Add(rule.ErrorMessage);
//             }
//         }

//         return errors;
//     }
// }

//   class Program{
//     static void Main()
//     {var emailValidator = new Validator<string>();

//         emailValidator.AddRule(value => value.Contains("@"), "Invalid email");
//         emailValidator.AddRule(value => value.Length >= 8, "Password too short");

//         var errors = emailValidator.Validate("test");

//         foreach (var error in errors)
//         {
//             Console.WriteLine(error);}
//     }}