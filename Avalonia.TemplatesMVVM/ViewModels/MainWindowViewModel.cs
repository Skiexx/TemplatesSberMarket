using System;
using System.Diagnostics;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using ReactiveUI;

namespace Avalonia.TemplatesMVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        CopyHeaderCommand = ReactiveCommand.Create(CopyHeaderCommandImpl);
        CopyFullTextCommand = ReactiveCommand.Create(CopyFullTextCommandImpl);
        this.WhenAnyValue(
            x => x.FirstName, 
            x => x.PhoneNumber, 
            x => x.ReasonComboBoxSelectedIndex,
            x => x.MorningRadioButton,
            x => x.DayRadioButton,
            x => x.ReasonTextBox).Subscribe(x =>
        {
            switch (ReasonComboBoxSelectedIndex)
            {
                case 0:
                {
                    FullText = (MorningRadioButton ? "Доброе утро" : DayRadioButton ? "Добрый день" : "Добрый вечер") +
                               (FirstName != "" ? ", " + FirstName : "") + "!\n"
                               + "К сожалению, нам не удалось связаться с вами по номеру, указанному в заказе: " +
                               (PhoneNumber != "" ? PhoneNumber : "<Тел. Кл.>") +
                               "\nМы делаем все возможное, чтобы вы были довольны нашим сервисом, но, так как " +
                               (ReasonTextBox != "" ? ReasonTextBox : "<Причина>") +
                               ", не получается доставить ваш заказ в выбранном вами интервале с " +
                               "<Время>" + ". Приносим наши искренние извинения!\n" +
                               "Мы перенесли ваш заказ на <Дата> с <Время>.\n" +
                               "Пожалуйста, свяжитесь с нами, если у вас остались вопросы или неудобно принять заказ в это время.\n" +
                               "Спасибо, что вы с нами!\n\n" +
                               "Справочная информация: номер заказа <Номер заказа> от <Дата>.";
                    break;
                }
                default:
                {
                    FullText = null;
                    break;
                }
            }
        });
    }

    public ReactiveCommand<Unit, Unit> CopyHeaderCommand { get; }

    public ReactiveCommand<Unit,Unit> CopyFullTextCommand { get; }

    private void CopyFullTextCommandImpl()
    {
        var clipboard = AvaloniaLocator.Current.GetService<IClipboard>();
        Debug.Assert(clipboard != null, nameof(clipboard) + " != null");
        clipboard.SetTextAsync(FullText!);
    }

    private void CopyHeaderCommandImpl()
    {
        var clipboard = AvaloniaLocator.Current.GetService<IClipboard>();
        Debug.Assert(clipboard != null, nameof(clipboard) + " != null");
        clipboard.SetTextAsync("Спасибо, что выбираете СберМаркет!");
    }

    private string? _firstName = "";
    private string? _phoneNumber = "";
    private string? _fullText = "";
    private int _reasonComboBoxSelectedIndex = -1;
    private bool _morningRadioButton = true;
    private bool _dayRadioButton;
    private DateTimeOffset _datePicker = DateTime.Now;
    private string? _reasonTextBox = "";

    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            this.RaisePropertyChanged();
        }
    }

    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            this.RaisePropertyChanged();
        }
    }

    public string? FullText
    {
        get => _fullText;
        set
        {
            _fullText = value;
            this.RaisePropertyChanged();
        }
    }

    public int ReasonComboBoxSelectedIndex
    {
        get => _reasonComboBoxSelectedIndex;
        set
        {
            _reasonComboBoxSelectedIndex = value;
            this.RaisePropertyChanged();
        }
    }

    public bool MorningRadioButton
    {
        get => _morningRadioButton;
        set
        {
            _morningRadioButton = value; 
            this.RaisePropertyChanged();
        }
    }

    public bool DayRadioButton
    {
        get => _dayRadioButton;
        set
        {
            _dayRadioButton = value; 
            this.RaisePropertyChanged();
        }
    }

    public DateTimeOffset DatePicker
    {
        get => _datePicker;
        set
        {
            _datePicker = value;
            this.RaisePropertyChanged();
        }
    }

    public string? ReasonTextBox
    {
        get => _reasonTextBox;
        set
        {
            _reasonTextBox = value; 
            this.RaisePropertyChanged();
        }
    }

    
}