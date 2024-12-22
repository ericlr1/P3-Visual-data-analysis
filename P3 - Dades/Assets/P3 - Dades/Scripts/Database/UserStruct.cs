using UnityEngine;

public struct User
{
    public static User CreateNewUser()
    {
        User user = new User();

        user.name = UserAttributes.GenerateRandomName();
        user.country = UserAttributes.GenerateRandomCountry();
        user.age = Random.Range(18, 99);
        user.gender = UserAttributes.GenerateRandomGender();

        return user;
    }

    public int userID;
    public string name;
    public string country;
    public int age;
    public string gender;
};
