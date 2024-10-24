package Printer;

public class GenericPrinter implements Printer {
    public GenericPrinter(){
        System.out.println("GenericPrinter is constructed");
    }
    @Override
    public void print(String message) {
        System.out.println(message);
    }
}
