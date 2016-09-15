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

	public string ToString() {
		return longitude + "," + latitude;
	}
}
