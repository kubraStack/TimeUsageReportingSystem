string password = "123456";
byte[] hash, salt;


Core.Utilities.Hashing.HashingHelper.CreatePasswordHash(password, out hash, out salt);

string hashBase64 = Convert.ToBase64String(hash);
string saltBase64 = Convert.ToBase64String(salt);
Console.WriteLine($"hash:{hashBase64 }");
Console.WriteLine($"salt:{saltBase64}");