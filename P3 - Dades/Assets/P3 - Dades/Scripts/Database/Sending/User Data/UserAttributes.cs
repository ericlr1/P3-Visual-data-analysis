using UnityEngine;

public class UserAttributes
{
    #region USER AVAILABLE DATA

    private static string[] names = 
    {
        "Alice", "Bob", "Charlie", "Diana", "Eve", "Frank", "Grace", "Hank", "Ivy", "Jack",
        "Karen", "Leo", "Mia", "Nina", "Oscar", "Penny", "Quincy", "Ruby", "Sam", "Tina",
        "Uma", "Victor", "Wendy", "Xander", "Yara", "Zane", "Adrian", "Bella", "Caleb",
        "Delilah", "Ethan", "Fiona", "Gavin", "Hazel", "Isaac", "Jenna", "Kai", "Lila",
        "Miles", "Nora", "Owen", "Phoebe", "Quinn", "Reid", "Sophie", "Theo", "Ursula",
        "Vivian", "Wyatt", "Ximena", "Yvonne", "Zachary", "Aaron", "Bianca", "Carter",
        "Daisy", "Edward", "Flora", "George", "Hannah", "Ian", "Jasmine", "Kieran",
        "Luna", "Mason", "Naomi", "Oliver", "Paige", "Riley", "Sawyer", "Tristan",
        "Umair", "Vanessa", "Wesley", "Xavier", "Yasmine", "Zoey", "Amber", "Brandon",
        "Chloe", "Derek", "Ella", "Felix", "Giselle", "Holden", "Isla", "Jonah", "Kara",
        "Levi", "Melody", "Nathan", "Olivia", "Parker", "Reese", "Sebastian", "Tyler",
        "Ulrich", "Valerie", "Warren", "Xander", "Yvette", "Zion", "Annie", "Blake",
        "Celia", "Damon", "Eliza", "Finn", "Gloria", "Hudson", "Irene", "Julian", "Kyla",
        "Logan", "Micah", "Noelle", "Orion", "Piper", "Rowan", "Stella", "Tanner",
        "Ulysses", "Veronica", "Willow", "Xenia", "Yvette", "Zoe", "Amelia", "Bryce",
        "Clara", "Declan", "Eloise", "Flynn", "Gemma", "Hugo", "Isabel", "James", "Kylie",
        "Liam", "Maxwell", "Natalie", "Omar", "Paisley", "Reagan", "Sarah", "Toby",
        "Ulani", "Violet", "Wren", "Ximena", "Yara", "Zara", "Aria", "Beau", "Camila",
        "Dean", "Emmett", "Freya", "Grayson", "Harper", "Jasper", "Kennedy",
        "Leila", "Mila", "Nolan", "Odelia", "Penelope", "Ronan", "Skye", "Tessa",
        "Uriah", "Vera", "Willa", "Xavier", "Yuna", "Zayden"
    };

    private static string[] countries = 
    {
        "USA", "Spain", "Germany", "Japan", "India", "Brazil", "France", "Canada",
        "Italy", "Australia", "Mexico", "China", "Russia", "United Kingdom", "South Korea",
        "Argentina", "South Africa", "Netherlands", "Sweden", "New Zealand", "Norway",
        "Ireland", "Switzerland", "Portugal", "Belgium", "Turkey", "Denmark", "Poland",
        "Austria", "Greece", "Singapore", "Thailand", "Malaysia", "Vietnam", "Indonesia",
        "Philippines", "Chile", "Colombia", "Peru", "Egypt", "Morocco", "Kenya",
        "Saudi Arabia", "United Arab Emirates", "Israel", "Czech Republic", "Hungary",
        "Romania", "Ukraine", "Finland", "Croatia", "Serbia", "Slovakia", "Bulgaria",
        "Estonia", "Latvia", "Lithuania", "Iceland", "Malta", "Cyprus", "Luxembourg",
        "Slovenia", "Monaco", "Liechtenstein", "Andorra", "San Marino", "Panama",
        "Costa Rica", "Uruguay", "Paraguay", "Ecuador", "Bolivia", "Honduras",
        "Guatemala", "El Salvador", "Nicaragua", "Cuba", "Dominican Republic",
        "Jamaica", "Trinidad and Tobago", "Barbados", "Bahamas", "Fiji", "Papua New Guinea",
        "Samoa", "Tonga", "Vanuatu", "Kiribati", "Maldives", "Seychelles", "Mauritius",
        "Madagascar", "Botswana", "Zimbabwe", "Zambia", "Namibia", "Mozambique",
        "Tanzania", "Uganda", "Rwanda", "Burundi", "Ethiopia", "Somalia", "Sudan",
        "Algeria", "Tunisia", "Libya", "Jordan", "Lebanon", "Qatar", "Oman", "Kuwait",
        "Bahrain", "Brunei", "Myanmar", "Bangladesh", "Sri Lanka", "Pakistan", "Nepal",
        "Bhutan", "Kazakhstan", "Uzbekistan", "Azerbaijan", "Armenia", "Georgia"
    };

    private static string[] genders = { "Male", "Female", "Non-binary" };

    #endregion

    public static string GenerateRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }

    public static string GenerateRandomCountry()
    {
        return countries[Random.Range(0, countries.Length)];
    }

    public static string GenerateRandomGender()
    {
        return genders[Random.Range(0, genders.Length)];
    }
}
