package Printer;

public class Logger {
    private GenericPrinter printer;
    private int level;

    public Logger(){
        printer = new GenericPrinter();
        level = 10000000;
    }

    public void addPrinter(GenericPrinter p){
        printer = p;
    }

    public void setLevel(int l){
        level = l;
    }

    public void log(int level, String message){
        if(level >= this.level){
            printer.print(message);
        }
    }
}
