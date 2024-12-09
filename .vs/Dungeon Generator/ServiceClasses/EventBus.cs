using System;
using System.Windows;
using System.Windows.Shapes;

/// <summary>
/// Статичный класс для событий, которые могут использоваться для обеспечения 
/// быстрого доступа к методам и переменным классов из других классов
/// </summary>
public static class EventBus
{
    /// <summary>
    /// Передает странице с сгенерированным подземельем данные о настройках для генерации
    /// </summary>
    public static Action<int, int, int, int> onSetGeneratorPage;

    /// <summary>
    /// Делегат - оно же, можно сказать, событие - для доступа к данным о готовой карте
    /// </summary>
    public static Func<Rectangle[,]> onGetMap;

    /// <summary>
    /// Позволяет из любого места в коде получить ссылку на главное окно
    /// </summary>
    public static Func<Window> onGetMainWindow;
}

