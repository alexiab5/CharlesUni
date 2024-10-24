package Printer;

public class TimestampPrinter extends GenericPrinter {
    public TimestampPrinter(){
        System.out.println("TimestampPrinter is constructed");
    }
    @Override
    public void print(String message) {
        super.print(message);
        System.out.println(new java.util.Date());
    }
}
