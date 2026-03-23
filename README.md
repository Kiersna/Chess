# ♟️ Chess

Desktopowa aplikacja szachowa napisana w **C# / WPF (.NET)**, z pełną logiką gry i graficznym interfejsem użytkownika.

---

## 📋 Spis treści

- [Jak działa program](#jak-działa-program)
- [Wymagania i funkcjonalności](#wymagania-i-funkcjonalności)
- [Instalacja i uruchomienie](#instalacja-i-uruchomienie)
- [Instrukcja obsługi](#instrukcja-obsługi)
- [Struktura projektu](#struktura-projektu)

---

## Jak działa program

Aplikacja składa się z dwóch oddzielnych modułów:

- **`ChessLogic`** — cała logika gry, niezależna od UI
- **`ChessUI`** — warstwa graficzna (WPF), obsługuje wyświetlanie i zdarzenia kliknięć

**Przepływ rozgrywki:**

```
Użytkownik klika bierkę
    → GameState.LegalMovesForPieces()
        → Piece.GetMoves() — generuje kandydatów na ruchy
        → Board.Copy() + move.Execute() — symuluje każdy ruch
        → Board.IsInCheck() — odrzuca ruchy zostawiające króla w szachu
    → UI podświetla dostępne pola

Użytkownik klika pole docelowe
    → GameState.MakeMove()
        → move.Execute() — wykonuje ruch, ustawia HasMoved = true
        → CurrentPlayer zmienia się na przeciwnika
        → CheckForGameOver() — sprawdza mat / pat
    → UI odświeża planszę
```

**Kluczowe klasy:**

| Klasa | Odpowiedzialność |
|-------|-----------------|
| `GameState` | Zarządzanie turami, filtrowanie legalnych ruchów, wykrywanie końca gry |
| `Board` | Stan planszy 8×8, kopiowanie, wykrywanie szacha |
| `Piece` + klasy pochodne | Logika ruchów każdego typu bierki |
| `NormalMove` | Wykonanie ruchu na planszy, aktualizacja `HasMoved` |
| `Result` | Wynik zakończonej gry (mat/pat, zwycięzca) |

---

## Wymagania i funkcjonalności

### ✅ Funkcjonalne

| # | Funkcjonalność | Status |
|---|---------------|--------|
| F1 | Plansza 8×8 z bierkami w pozycji startowej | ✅ |
| F2 | Ruchy wszystkich 6 typów bierek zgodnie z zasadami | ✅ |
| F3 | Blokowanie ruchów wystawiających własnego króla na szach | ✅ |
| F4 | Ruch pionem o 2 pola przy pierwszym ruchu | ✅ |
| F5 | Wykrywanie szacha | ✅ |
| F6 | Wykrywanie mata — zakończenie gry z ogłoszeniem zwycięzcy | ✅ |
| F7 | Wykrywanie pata — zakończenie gry z ogłoszeniem remisu | ✅ |
| F8 | Podświetlanie dostępnych ruchów po wybraniu bierki | ✅ |
| F9 | Naprzemienne tury (biały zaczyna) | ✅ |

### ⚙️ Niefunkcjonalne

- Aplikacja działa na systemie **Windows** (WPF / .NET 6+)
- Separacja logiki od UI — `ChessLogic` można wykorzystać niezależnie
- Architektura umożliwia łatwe dodanie nowych typów ruchów (roszada, en passant, promocja pionka)

---

## Instalacja i uruchomienie

**Wymagania:**
- Windows 10 / 11
- [.NET 6 SDK](https://dotnet.microsoft.com/download) lub nowszy
- Visual Studio 2022

**Kroki:**

```bash
git clone https://github.com/twoj-username/chess.git
cd chess
```

Otwórz `Chess.sln` w Visual Studio, ustaw `ChessUI` jako projekt startowy i naciśnij **F5**.

---

## Instrukcja obsługi

1. Uruchom aplikację — pojawi się plansza w pozycji startowej, grę rozpoczyna **biały**
2. Kliknij swoją bierkę — dostępne pola zostaną **podświetlone**
3. Kliknij podświetlone pole aby **wykonać ruch**
4. Tura przechodzi automatycznie na przeciwnika
5. Gra kończy się komunikatem o **macie** (wygrana) lub **pacie** (remis)

> **Uwaga:** Nie można wykonać ruchu który wystawia własnego króla na szach. Takie ruchy nie będą podświetlone.

---

## Struktura projektu

```
Chess.sln
├── ChessLogic/
│   ├── Moves/
│   │   ├── Move.cs               # Klasa bazowa ruchu
│   │   └── NormalMove.cs         # Standardowy ruch bierki
│   ├── Pieces/
│   │   ├── Piece.cs              # Klasa bazowa bierki
│   │   ├── Pawn.cs
│   │   ├── King.cs
│   │   ├── Queen.cs
│   │   ├── Rook.cs
│   │   ├── Bishop.cs
│   │   └── Knight.cs
│   ├── Board.cs                  # Stan planszy, wykrywanie szacha
│   ├── GameState.cs              # Logika gry, mat, pat
│   ├── Result.cs                 # Wynik gry
│   ├── Direction.cs
│   ├── Position.cs
│   ├── Player.cs
│   ├── PieceType.cs
│   └── MoveType.cs
└── ChessUI/
    ├── Assets/                   # Grafiki bierek
    ├── App.xaml
    ├── MainWindow.xaml           # Główne okno aplikacji
    └── Images.cs                 # Ładowanie grafik z zasobów
```
