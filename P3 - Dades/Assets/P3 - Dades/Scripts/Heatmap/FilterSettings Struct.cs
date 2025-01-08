using System.Collections.Generic;

[System.Flags]
public enum FilterType
{
    None = 0,          // 0000 (binario)
    Country = 1 << 0,  // 0001 (binario) = 1
    Gender = 1 << 1,   // 0010 (binario) = 2
    Age = 1 << 2       // 0100 (binario) = 4
}
public struct FilterSettings
{
    public FilterType activeFilters;

    public int selectedCountryIndex;

    public int selectedGenderIndex;

    public int minAge;
    public int maxAge;

    public int selectedDataIndex;
}
