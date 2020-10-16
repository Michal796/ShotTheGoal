# ShotTheGoal
Założenia gry:
Celem gry trafienie pociskiem do celu, będącego zielonym prostokątem w środku zamku, przy oddaniu jak najmniejszej ilości strzałów. Pocisk wystrzeliwany jest z procy. Zamki zbudowane są z elementów szarych (niezniszczalnych), oraz brązowych które ulegają zniszczeniu jeśli prędkość uderzającego w nie pocisku jest odpowiednio wysoka. Po trafieniu pociskiem do celu rozpocznie się kolejny poziom gry. W sumie w grze dostępne są 4 poziomy.

Sterowanie: sterowanie odbywa się wyłącznie przy pomocy myszki. Wciśnięcie lewego przycisku myszy powoduje przejście w tryb celowania (należy kliknąć na procę). 
Zwolnienie przycisku spowoduje wystrzał z procy.

Skrypty:
- CamFollow - klasa odpowiada za śledzenie wystrzelonego pocisku kamerą, oraz powrót do widoku procy gdy pocisk przestanie się ruszać.
- Cloud - klasa odpowiedzialna za wygenerowanie chmury korzystając z prefabrykantu cloudSphere (kuli).
- CloudCrafter - klasa odpowiada za wygenerowanie chmur na podstawie obiektu cloudPrefab.
- Goal - klasa przypisana do celu, odpowiada za graficzną informację o trafieniu do celu (zmiana koloru), oraz określa pole goalMet, na podstawie którego następuje        zmiana poziomu.
- Projectile - klasa odpowiada ze niszczenie drewnianych klocków, gdy prędkość pocisku w chwili uderzenia jest odpowiednio wysoka.
- ProjectileLine = klasa odpowiada za rysowanie linii będącej śladem toru lotu pocisku.
- RigidbodySleep - klasa będąca komponentem obiektów, z których zbudowane są zamki. Zapewnia stabilność zamku do momentu uderzenia pocisku.
- ShotTheGoal - klasa umożliwia kontrolę aktualnego poziomu gry i interfejsu oraz możliwość wybrania położenia kamery.
- Slingshot - klasa odpowiada za celowanie procą oraz strzelanie.
