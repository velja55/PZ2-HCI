using System.Windows.Input;

namespace NetworkService.Helpers
{
    public static class Commands
    {
        // Undo view - Ctrl Z (na sve)
        public static RoutedCommand UndoKbd = new RoutedCommand();

        // Graf za prvi entitet - Ctrl Q (samo na Measurement Graph)
        public static RoutedCommand ShowGraphKbd = new RoutedCommand();

        // Obriši izabrani - Ctrl W (samo na Entites view)
        public static RoutedCommand DeleteKbd = new RoutedCommand();

        // Skini sa kanvasa - Ctrl E (samo na Display view)
        public static RoutedCommand RemoveFromCanvasKbd = new RoutedCommand();
    }
}
