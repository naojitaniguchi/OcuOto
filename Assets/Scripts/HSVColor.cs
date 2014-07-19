using UnityEngine;
using System.Collections;

public class HSVColor {
	
	// h = 0.0 -> 360.0, s = 0.0 -> 1.0, v = 0.0 -> 1.0
	public static Color HSVToRGB(float h, float s, float v) {
		if(s == 0) {
			return new Color(v, v, v, 1);
		}
		h /= 60.0f;
		var sector = Mathf.FloorToInt(h);
		var fact = h - sector;
		var p = v * (1.0f - s);
		var q = v * (1.0f - s * fact);
		var t = v * (1 - s * (1 - fact));
		
		switch(sector) {
		case 0:
			return new Color(v, t, p, 1);
		case 1:
			return new Color(q, v, p, 1);
		case 2:
			return new Color(p, v, t, 1);
		case 3:
			return new Color(p, q, v, 1);
		case 4:
			return new Color(t, p, v, 1);
		default:
			return new Color(v, p, q, 1);
		}
	}
}