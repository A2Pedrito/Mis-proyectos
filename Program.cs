int num;
string[] nombres;
int[] NumCuenta;
int[] Saldos;
System.Console.Write("Cuantos clientes vas a ingresar (min 1, max 3): ");
do
{
    int.TryParse(Console.ReadLine(), out num);
    if (num < 1 || num > 3)
    {
        System.Console.Write("Ingrese la cantidad de clientes dentro del rango: ");
    }
} while (num < 1 || num > 3);

Console.Clear();

nombres = new string[num];
NumCuenta = new int[num];
Saldos = new int[num];

int Desicion;
Registro_Cliente(num, nombres, NumCuenta, Saldos);
do
{
    Console.Clear();
    Menu();
    int.TryParse(Console.ReadLine(), out Desicion);
    switch (Desicion)
    {
        case 1:
            Deposito(num, NumCuenta, Saldos);
            VolverMenu();
            break;
        case 2:
            Retiro(num, NumCuenta, Saldos);
            VolverMenu();
            break;
        case 3:
            Transferencia(NumCuenta, Saldos, num);
            VolverMenu();
            break;
        case 4:
            Consulta(nombres, NumCuenta, Saldos, num);
            VolverMenu();
            break;
        case 5:
            System.Console.WriteLine("Saliendo del programa.");
            Environment.Exit(0);
            break;
        default:
            System.Console.WriteLine("Esa opcion no se encuentra en el menu. ");
            VolverMenu();
            break;
    }
} while (Desicion != 5);




static void Registro_Cliente(int num, string[] nombres, int[] NumCuenta, int[] Saldos)
{
    int Intento, L = 1;
    bool listo;

    for (int i = 0; i < num; i++)
    {
        System.Console.Write($"{L}-) Ingrese el nombre del cliente: ");
        nombres[i] = Console.ReadLine()!;

        System.Console.Write($"{L}-) Ingrese el numero de cuenta: ");
        do
        {
            listo = true;
            int.TryParse(Console.ReadLine(), out Intento);
            for (int j = 0; j < i; j++)
            {
                if (Intento == NumCuenta[j])
                {
                    System.Console.Write("Numero de cuenta existente. Vuelve a intentar: ");
                    listo = false;
                    break;
                }
            }
        } while (!listo);
        NumCuenta[i] = Intento;

        System.Console.Write($"{L}-) Ingresa el Saldo incial: ");
        do
        {
            listo = true;
            int.TryParse(Console.ReadLine(), out int Saldo);
            if (Saldo < 0)
            {
                System.Console.Write("El saldo debe ser mayor a 0. Vuelve a intentarlo: ");
                listo = false;
            }
            else
            {
                Saldos[i] = Saldo;
            }
        } while (!listo);
        L += 1;
        System.Console.WriteLine();
    }
}

static void Deposito(int num, int[] NumCuenta, int[]Saldos)
{
    int indiceOrigen = -1;
    int montoDeposito;
    System.Console.Write("Ingrese el numero de cuenta a Depositar: ");
    int.TryParse(Console.ReadLine(), out int Cuenta);
    for (int i = 0; i < num; i++)
    {
        if (Cuenta == NumCuenta[i])
        {
            indiceOrigen = i;
            break;
        }
    }
    if (indiceOrigen == -1)
    {
        System.Console.WriteLine("El numero de cuenta no existe. Vuelva a realizar el proceso nuevamente.");
        return;
    }
    if (indiceOrigen != -1)
    {
        System.Console.Write("Ingresa el monto de deposito: ");
        int.TryParse(Console.ReadLine(), out montoDeposito);

        if (montoDeposito > 0)
        {
            Saldos[indiceOrigen] += montoDeposito;
            System.Console.WriteLine("Deposito exitoso.");
        }
        else
        {
            System.Console.WriteLine("El monto del deposito debe ser mayor a 0");
        }
    }
} 

static void Retiro(int num, int[] NumCuenta, int[] Saldos)
{
    int indiceOrigen = -1;
    int montoRetiro;
    System.Console.Write("Ingrese el numero de cuenta a Retirar: ");
    int.TryParse(Console.ReadLine(), out int Cuenta);
    for (int i = 0; i < num; i++)
    {
        if (Cuenta == NumCuenta[i])
        {
            indiceOrigen = i;
            break;
        }
    }
    if (indiceOrigen == -1)
    {
        System.Console.WriteLine("El numero de cuenta no existe. Vuelva a realizar el proceso nuevamente.");
        return;
    }
    if (indiceOrigen != -1)
    {
        System.Console.Write("Ingresa el monto de retiro: ");
        int.TryParse(Console.ReadLine(), out montoRetiro);

        if (montoRetiro <= Saldos[indiceOrigen] && montoRetiro != 0)
        {
            Saldos[indiceOrigen] -= montoRetiro;
            System.Console.WriteLine("Retiro exitoso.");
        }
        else if (montoRetiro == 0)
        {
            System.Console.WriteLine("ERROR al retirar el dinero. Vuelva a realizar el proceso nuevamente.");
        }
        else
        {
            System.Console.WriteLine("No se puede retirar un saldo mayor al que hay. Vuelva a realizar el proceso.");
        }
    }
}

static void Transferencia(int[] NumCuenta, int[] Saldos, int num)
{
    int Cuenta, Cuenta2, Salida;
    int indiceDestino = -1;
    int indiceOrigen = -1;

    System.Console.Write("Ingrese el numero de cuenta de transferencia: ");
    int.TryParse(Console.ReadLine(), out Cuenta);
    for (int i = 0; i < num; i++)
    {
        if (Cuenta == NumCuenta[i])
        {
            indiceOrigen = i;
            break;
        }
    }
    if (indiceOrigen == -1)
    {        
        System.Console.WriteLine("El numero de cuenta no existe. Vuelva a realizar el proceso nuevamente.");
    }
    if (indiceOrigen != -1)
    {
        System.Console.Write("Ahora Ingrese el numero de cuenta a depositar: ");
        int.TryParse(Console.ReadLine(), out Cuenta2);
        for (int j = 0; j < num; j++)
        {
            if (Cuenta2 == NumCuenta[j] && Cuenta2 != Cuenta)
            {
                indiceDestino = j;
                break;
            }
        }
        if (indiceDestino == -1)
        {
            System.Console.WriteLine("El numero de cuenta no existe. Vuelva a realizar el proceso nuevamente.");
        }
        if (indiceDestino != -1)
        {

            System.Console.Write("Ingrese el monto de transferencia: ");
            int.TryParse(Console.ReadLine(), out Salida);
            if (Salida <= Saldos[indiceOrigen] && Salida > 0)
            {
                Saldos[indiceOrigen] -= Salida;
                Saldos[indiceDestino] += Salida;
                System.Console.WriteLine("Transferencia exitosa."); 
            }
            else if (Salida <= 0)
            {
                System.Console.WriteLine("El monto de deposito no puede ser igual o menor a 0.");
                System.Console.WriteLine("Vuelva a realizar el proceso.");
            }
            else
            {
                System.Console.WriteLine("No se puede transferir una cantidad mayor a la que hay en la cuenta.");
                System.Console.WriteLine("Vuelva a realizar el proceso.");
            }
        }
    }
}

static void Consulta(string[] nombres, int[] NumCuenta, int[] Saldos, int num)
{
    for (int i = 0; i < num; i++)
    {
        System.Console.WriteLine($"Nombre: {nombres[i]}");
        System.Console.WriteLine($"Numero de cuenta: {NumCuenta[i]}");
        System.Console.WriteLine($"Saldo: {Saldos[i]}");
        System.Console.WriteLine();
    }
}

static void Menu()
{
    System.Console.WriteLine("Opciones a Realizar: ");
    System.Console.WriteLine("[1] Deposito");
    System.Console.WriteLine("[2] Retiro");
    System.Console.WriteLine("[3] Transferencia");
    System.Console.WriteLine("[4] Consulta");
    System.Console.WriteLine("[5] Salir");
}

static void VolverMenu()
{
    System.Console.WriteLine("Presione cualquier tecla para volver al menu y realizar el proceso.");
    Console.ReadKey();
}