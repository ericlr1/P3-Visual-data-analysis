using System.Collections.Generic;

public struct MapFilterSettings
{
    // TODO: Podriamos cambiar esto por un diccionario o algo asi para no
    // tener que mirar toda la información del Struct cada vez que querramos aplicar los filtros

    //public List<bool> filters;

    public int selectedCountryIndex;

    public int selectedGenderIndex;

    public int minAge;
    public int maxAge;
}
