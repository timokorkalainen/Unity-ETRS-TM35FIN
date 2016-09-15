public class ETRSTM35FIN {

	static double Ca = 6378137.0;
	static double Cf = 1.0 / 298.257223563;
	static double Ck0 = 0.9996;
	static double Clo0 = DegreeToRadian(27.0);
	static double CE0 = 500000.0;
	static double Cn = Cf / (2.0 - Cf);
	static double CA1 = Ca / (1.0 + Cn) * (1.0 + (System.Math.Pow(Cn, 2.0)) / 4.0 + (System.Math.Pow(Cn, 4.0)) / 64.0);
	static double Ce = System.Math.Sqrt((2.0 * Cf - System.Math.Pow(Cf, 2.0)));
	static double Ch1 = 1.0 / 2.0 * Cn - 2.0 / 3.0 * (System.Math.Pow(Cn, 2.0)) + 37.0 / 96.0 * (System.Math.Pow(Cn, 3.0)) - 1.0 / 360.0 * (System.Math.Pow(Cn, 4.0));
	static double Ch2 = 1.0 / 48.0 * (System.Math.Pow(Cn, 2.0)) + 1.0 / 15.0 * (System.Math.Pow(Cn, 3.0)) - 437.0 / 1440.0 * (System.Math.Pow(Cn, 4.0));
	static double Ch3 = 17.0 / 480.0 * (System.Math.Pow(Cn, 3.0)) - 37.0 / 840.0 * (System.Math.Pow(Cn, 4.0));
	static double Ch4 = 4397.0 / 161280.0 * (System.Math.Pow(Cn, 4.0));

	static double Ch1p = 1.0/2.0 * Cn- 2.0/3.0 * System.Math.Pow(Cn, 2.0) + 5.0/16.0 * System.Math.Pow(Cn, 3.0) + 41.0/180.0 * System.Math.Pow(Cn, 4.0);
	static double Ch2p = 13.0/48.0 * System.Math.Pow(Cn, 2.0) - 3.0/5.0 * System.Math.Pow(Cn, 3.0) + 557.0/1440.0 * System.Math.Pow(Cn, 4.0);
	static double Ch3p = 61.0/240.0 * System.Math.Pow(Cn, 3.0) - 103.0/140.0 * System.Math.Pow(Cn, 4.0);
	static double Ch4p = 49561.0/161280.0 * System.Math.Pow(Cn, 4.0);

	static double tm35fin_latitude_min = 6582464.0358;
	static double tm35fin_latitude_max = 7799839.8902;
	static double tm35fin_longitude_min = 50199.4814;
	static double tm35fin_longitude_max = 761274.6247;

	public static bool WithinTM35FIN(Geolocation etrs) {
		if (etrs.latitude < tm35fin_latitude_min || etrs.latitude > tm35fin_latitude_max) { //
			return false;
		}

		if (etrs.longitude < tm35fin_longitude_min || etrs.longitude > tm35fin_longitude_max) {
			return false;
		}

		return true;
	}

	public static Geolocation toWGS84(Geolocation etrs) {

		if (!WithinTM35FIN (etrs))
			return Geolocation.zero;

		double E = etrs.latitude / (CA1 * Ck0);
		double nn = (etrs.longitude - CE0) / (CA1 * Ck0);
		double E1p = Ch1 * System.Math.Sin(2.0 * E) * System.Math.Cosh(2.0 * nn);
		double E2p = Ch2 * System.Math.Sin(4.0 * E) * System.Math.Cosh(4.0 * nn);
		double E3p = Ch2 * System.Math.Sin(6.0 * E) * System.Math.Cosh(6.0 * nn);
		double E4p = Ch3 * System.Math.Sin(8.0 * E) * System.Math.Cosh(8.0 * nn);

		double nn1p = Ch1 * System.Math.Cos(2.0 * E) * System.Math.Sinh(2.0 * nn);
		double nn2p = Ch2 * System.Math.Cos(4.0 * E) * System.Math.Sinh(4.0 * nn);
		double nn3p = Ch3 * System.Math.Cos(6.0 * E) * System.Math.Sinh(6.0 * nn);
		double nn4p = Ch4 * System.Math.Cos(8.0 * E) * System.Math.Sinh(8.0 * nn);

		double Ep = E - E1p - E2p - E3p - E4p;

		double nnp = nn - nn1p - nn2p - nn3p - nn4p;
		double be = System.Math.Asin(System.Math.Sin(Ep) / System.Math.Cosh(nnp));

		double Q = Asinh(System.Math.Tan(be));
		double Qp = Q + Ce * Atanh(Ce * System.Math.Tanh(Q));
		Qp = Q + Ce * Atanh(Ce * System.Math.Tanh(Qp));
		Qp = Q + Ce * Atanh(Ce * System.Math.Tanh(Qp));
		Qp = Q + Ce * Atanh(Ce * System.Math.Tanh(Qp));

		double latitude = RadianToDegree(System.Math.Atan(System.Math.Sinh(Qp)));

		double longitude = RadianToDegree(Clo0 + System.Math.Asin(System.Math.Tanh(nnp) / System.Math.Cos(be)));

		return new Geolocation(longitude, latitude);
	}

	public static Geolocation toETRSTM35FIN(Geolocation wgs84) {
		
		double la = DegreeToRadian(wgs84.latitude);
		double lo = DegreeToRadian(wgs84.longitude);

		double Q = Asinh(System.Math.Tan(la)) - Ce * Atanh(Ce* System.Math.Sin(la));
		double be = System.Math.Atan(System.Math.Sinh(Q));
		double nnp = Atanh(System.Math.Cos(be) * System.Math.Sin(lo - Clo0));
		double Ep = System.Math.Asin(System.Math.Sin(be) * System.Math.Cosh(nnp));
		double E1 = Ch1p* System.Math.Sin(2.0 * Ep) * System.Math.Cosh(2.0 * nnp);
		double E2 = Ch2p* System.Math.Sin(4.0 * Ep) * System.Math.Cosh(4.0 * nnp);
		double E3 = Ch3p* System.Math.Sin(6.0 * Ep) * System.Math.Cosh(6.0 * nnp);
		double E4 = Ch4p* System.Math.Sin(8.0 * Ep) * System.Math.Cosh(8.0 * nnp);

		double nn1 = Ch1p* System.Math.Cos(2.0 * Ep) * System.Math.Sinh(2.0 * nnp);
		double nn2 = Ch2p* System.Math.Cos(4.0 * Ep) * System.Math.Sinh(4.0 * nnp);
		double nn3 = Ch3p* System.Math.Cos(6.0 * Ep) * System.Math.Sinh(6.0 * nnp);
		double nn4 = Ch4p* System.Math.Cos(8.0 * Ep) * System.Math.Sinh(8.0 * nnp);
		double E = Ep + E1 + E2 + E3 + E4;
		double nn = nnp + nn1 + nn2 + nn3 + nn4;

		double etrs_latitude = CA1* E * Ck0;
		double etrs_longitude = CA1* nn * Ck0+ CE0;

		Geolocation etrs = new Geolocation(etrs_longitude, etrs_latitude);

		if (!WithinTM35FIN (etrs))
			return Geolocation.zero;

		return etrs;
	}

	public static double Atanh(double value) {
		return System.Math.Log((1 / value + 1) / (1 / value - 1)) / 2;
	}

	public static double Asinh(double value) {
		return System.Math.Log(value + System.Math.Sqrt(value * value + 1));
	}

	public static double DegreeToRadian(double angle)
	{
		return (System.Math.PI / 180) * angle;
	}

	private static double RadianToDegree(double angle)
	{
		return angle * (180.0 / System.Math.PI);
	}

}