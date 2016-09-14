# Unity-ETRS-TM35FIN
Simple C# class for Unity 3D to convert coordinates between WGS84 (EUREF-FIN) and ETRS-TM35FIN systems.

The official ETRS-TM35FIN definition & conversion functions: http://www.jhs-suositukset.fi/web/guest/jhs/projects/jhs154

An example Geolocation class is also provided for convenience and conversions to Unity Vector2.

# Acknowledgements
Most of the code is based on the work of Olli Lammi's [coordinates](http://olammi.iki.fi/sw/coordinates/) Python library.
Other implementations referred and utilized in writing the code:
* http://search.cpan.org/~mplattu/Geo-Coordinates-ETRSTM35FIN-0.01/lib/Geo/Coordinates/ETRSTM35FIN.pm
* https://github.com/kakoni/etrstm35fin
* https://github.com/hkurhinen/etrs-tm35fin-to-wgs84-converter