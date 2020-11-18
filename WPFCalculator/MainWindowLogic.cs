using Calculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalculator
{
    public class MainWindowLogic
    {
        private long _currentValue;
        private CalculatorOperation _lastOperation;
        private int _firstValue;
        private bool _clearValue;
        private CalculatorLogic _calculatorLogic;
        private string _path;
        private string _file;
        public MainWindowLogic()
        {
            _calculatorLogic = new CalculatorLogic();
        }

        public bool NumericButtonAction(string number) // sprawdzamy przycisk wciśnięty przez użytkownika do którego za każdym razem odwołujemy się w klasie MainWindow
        {
            try
            {
                if (_clearValue) // jeśli _clearValue = true
                {
                    _currentValue = 0; // zamieniamy obecną wartość na 0 i zmieniamy _clearValue na false, ponieważ oznacza to, że póki co użytkownik jeszcze nie kliknął żadnego przycisku
                    _clearValue = false;
                }
                if (int.TryParse($"{_currentValue}{number}", out int res)) // parsujemy obecną wartość i numer na typ int
                {
                    _currentValue = res; // _currentValue = res
                    return true; // zwracamy prawdę dzięki czemu wyświetli nam się liczba na ekranie kalkulatora
                }
                return false; // w wypadku gdy powyższy warunek zostanie z jakiegoś powodu ominięty zwracany jest fałsz i nie wyświetli się nic na ekranie
            }
            catch (Exception ex)
            {
                _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "logs");
                _file = DateTime.Now.ToString("dd.MM.yyy") + ".txt";
                string fullpath = Path.Combine(_path, _file);

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                File.WriteAllText(fullpath, ex.Message);               
                throw;
            }
        }

        public bool OperationButtonAction(CalculatorOperation operation) // sprawdzana jest jedna z opcji kalkulatora
        {
            try
            {
                switch (operation)
                {
                    case CalculatorOperation.Result: // jeśli klikniemy = wrzyci nas do innej metody sprawdzającej ostatnią zapisaną operację w _lastOperation
                        return InnerOperationButtonAction(_lastOperation);
                    default:
                        return InnerOperationButtonAction(operation); // w każdym innym wypadku przeżuci nas do innej metody ale już z bieżącą operacją
                }
            }

            catch (Exception ex)
            {
                _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "logs");
                _file = DateTime.Now.ToString("dd.MM.yyy") + ".txt";
                string fullpath = Path.Combine(_path, _file);

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                File.WriteAllText(fullpath, ex.Message);
                throw;
            }

        }

        public int GetCurrentValue()
        {
            try
            {
                return Convert.ToInt32(_currentValue);
            }

            catch (Exception ex)
            {
                _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "logs");
                _file = DateTime.Now.ToString("dd.MM.yyy") + ".txt";
                string fullpath = Path.Combine(_path, _file);

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                File.WriteAllText(fullpath, ex.Message);
                throw;
            }
        }

        private bool InnerOperationButtonAction(CalculatorOperation operation)
        {
            try
            {
                switch (operation)
                {
                    case CalculatorOperation.Add: // jeśl dodajemy
                        _lastOperation = operation; // przypisujemy ostatniej operacji operację bieżącą
                        if (_firstValue == 0) // jeśli pierwsza wartość = 0 to po prostu pobieramy obecną wartość, zmieniamy _clearValue na true
                        {
                            _firstValue = GetCurrentValue();
                            _clearValue = true;
                        }
                        else // w przeciwnym wypadku dodajemy do pierwszej wartości, wartość już przechowywaną i przypisujemy nową wartość, po czym zmieniamy pierwszą wartość na 0
                        {
                            _currentValue = _calculatorLogic.Add(_firstValue, GetCurrentValue());
                            _firstValue = 0;
                        }
                        break;

                    case CalculatorOperation.Subtract:
                        _lastOperation = operation;
                        if (_firstValue == 0)
                        {
                            _firstValue = GetCurrentValue();
                            _clearValue = true;
                        }
                        else
                        {
                            _currentValue = _calculatorLogic.Subtract(_firstValue, GetCurrentValue());
                            _firstValue = 0;
                        }
                        break;

                    case CalculatorOperation.Divide:
                        _lastOperation = operation;
                        if (_firstValue == 0)
                        {
                            _firstValue = GetCurrentValue();
                            _clearValue = true;
                        }
                        else
                        {
                            _currentValue = _calculatorLogic.Divide(_firstValue, GetCurrentValue());
                            _firstValue = 0;
                        }
                        break;

                    case CalculatorOperation.Multiply:
                        _lastOperation = operation;
                        if (_firstValue == 0)
                        {
                            _firstValue = GetCurrentValue();
                            _clearValue = true;
                        }
                        else
                        {
                            _currentValue = _calculatorLogic.Multiply(_firstValue, GetCurrentValue());
                            _firstValue = 0;
                        }
                        break;

                    case CalculatorOperation.ChangeTo:
                        _lastOperation = operation;
                        _currentValue = _calculatorLogic.ChangeTo(GetCurrentValue());
                        break;

                    case CalculatorOperation.ButtonCE:
                        _lastOperation = operation;
                        _clearValue = true;
                        _currentValue = 0;
                        break;

                    case CalculatorOperation.Backspace:
                        _lastOperation = operation;
                        string s = GetCurrentValue().ToString();

                        if (s.Length > 0)
                        {
                            s = s.Remove(s.Length - 1, 1);
                            if (s.Length == 0)
                                _currentValue = 0;
                            else
                                _currentValue = long.Parse(s);
                        }
                        break;

                    case CalculatorOperation.ButtonC:
                        _lastOperation = operation;
                        if (_firstValue == 0)
                        {
                            _currentValue = 0;
                            _clearValue = true;
                        }

                        if (_firstValue != 0)
                        {
                            _currentValue = _firstValue;
                            _firstValue = 0;
                        }
                        break;

                }
                return true;
            }

            catch (Exception ex)
            {
                _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "logs");
                _file = DateTime.Now.ToString("dd.MM.yyy") + ".txt";
                string fullpath = Path.Combine(_path, _file);

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                File.WriteAllText(fullpath, ex.Message);
                throw;
            }
        }
    }
}
