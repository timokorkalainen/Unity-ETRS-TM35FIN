using UnityEngine;

/*
 * Provided for convenience, provide your own as seen fit
 * 
 */

[System.Serializable]
public class Geolocation {
	public double longitude;
	public double latitude;
	static public Geolocation zero = new Geolocation (0, 0);
	
	public Geolocation() {
	}

	public Geolocation(double longitude, double latitude) {
		this.longitude = longitude;
		this.latitude = latitude;
	}

	public Geolocation(Vector2 geolocation) {
		this.longitude = (double)geolocation.x;
		this.latitude = (double)geolocation.y;
	}

	public Vector2 ToVector2() {
		return new Vector2((float)longitude, (float)latitude);
	}

	override public string ToString() {
		return longitude + "," + latitude;
	}

    public override bool Equals(object obj)
    {
        return this.Equals(obj as Geolocation);
    }

    public bool Equals(Geolocation p)
    {
        // If parameter is null, return false.
        if (Object.ReferenceEquals(p, null))
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (longitude == p.longitude) && (latitude == p.latitude);
    }
}
