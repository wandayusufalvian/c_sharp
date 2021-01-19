//print string
string firstFriend ="Maria"; 
string secondFriend="Sage"; 
Console.WriteLine($"My friends are {firstFriend} and {secondFriend} ");

Console.WriteLine($"{firstFriend} has {firstFriend.Length} letters");
Console.WriteLine($"{secondFriend} has {secondFriend.Length} letters");

//Trim
string greeting="     Heello world     ";
string trimmmedGreeting=greeting.TrimStart();
Console.WriteLine($"[{trimmmedGreeting}]"); 

trimmmedGreeting=greeting.TrimEnd();
Console.WriteLine($"[{trimmmedGreeting}]");

trimmmedGreeting=greeting.Trim();
Console.WriteLine($"[{trimmmedGreeting}]");

//Replace 
string sayHello="Hello World!"; 
Console.WriteLine(sayHello);
sayHello=sayHello.Replace("Hello","Greetings");
Console.WriteLine(sayHello); 


//Upper and Lower 
string sayHell="HELL";
string sayHell2="hell";
Console.WriteLine(sayHell.ToLower());
Console.WriteLine(sayHell2.ToUpper()); 

//Contains 
string songLyrics="You say goodbye, and I say hello"; 
Console.WriteLine(songLyrics.Contains("goodbye")); 
Console.WriteLine(songLyrics.Contains("says")); 

//startwith and endwith 
Console.WriteLine(songLyrics.StartsWith("You")); 
Console.WriteLine(songLyrics.StartsWith("goodbye")); 
Console.WriteLine(songLyrics.EndsWith("hello")); 
Console.WriteLine(songLyrics.EndsWith("goodbye")); 






