
using System;

[Serializable]
public struct CuadriculaPosicion : IEquatable<CuadriculaPosicion>
{

    public int x;
    public int z;

    public CuadriculaPosicion(int x , int z)
    {

        this.x = x;
        this.z = z;

    }

    public override bool Equals(object obj)
    {
        return obj is CuadriculaPosicion posicion &&
               x == posicion.x &&
               z == posicion.z;
    }

    public bool Equals(CuadriculaPosicion other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public override string ToString()
    {
        return "x:" + x + "; z :" + z;
    }

    public static bool operator == (CuadriculaPosicion a, CuadriculaPosicion b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(CuadriculaPosicion a, CuadriculaPosicion b)
    {
        return !(a == b);
    }

    public static CuadriculaPosicion operator +(CuadriculaPosicion a, CuadriculaPosicion b)
    {

        return new CuadriculaPosicion(a.x + b.x, a.z + b.z);

    }

    public static CuadriculaPosicion operator -(CuadriculaPosicion a, CuadriculaPosicion b)
    {

        return new CuadriculaPosicion(a.x - b.x, a.z - b.z);

    }

}
