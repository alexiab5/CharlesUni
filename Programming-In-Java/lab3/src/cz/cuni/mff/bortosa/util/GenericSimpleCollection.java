package cz.cuni.mff.bortosa.util;
import java.util.Iterator;

public class GenericSimpleCollection implements SimpleCollection, Iterable {
    private Object[] collection;
    private int capacity;
    private int size;

    public GenericSimpleCollection() {
        capacity = 100;
        size = 0;
        collection = new Object[capacity];
    }

    /**
     * Method adds a new object to the collection.
     * @param o - the object to be added.
     */
    @Override
    public void add(Object o) {
        if(size == capacity) {
            // resize
            capacity *= 2;
            Object[] temp = new Object[capacity];
            System.arraycopy(collection, 0, temp, 0, collection.length);
            collection = temp;
        }
        collection[size] = o;
        size++;
    }

    /**
     * Method returns the object at index i in the collection. Throws IndexOutOfBoundsException if the index is invalid
     * @param i - the index of the objects
     */
    @Override
    public Object get(int i){
        if(i < 0 || i >= size)
            throw new IndexOutOfBoundsException();
        return collection[i];
    }

    /**
     * Method removes the Object o from the collection. If the object does not exist in the collection, nothing happens.
     * @param o - the object to be removed
     */
    @Override
    public void remove(Object o){
        int pos = -1;
        for(int i = 0; i < size; i++){
            if(collection[i].equals(o)){
                pos = i;
                break;
            }
        }
        for(int i = pos; i < size - 1; i++){
            collection[i] = collection[i + 1];
        }
        collection[size - 1] = null;
        size--;
    }

    /**
     * Method removes the object found at the specified index from the collection.
     * Throws IndexOutOfBoundsException if the index is invalid.
     * @param index - the index of the object to be removed.
     */
    @Override
    public void remove(int index){
        if(index < 0 || index >= size)
            throw new IndexOutOfBoundsException();
        for(int i = index; i < size - 1; i++){
            collection[i] = collection[i + 1];
        }
        collection[size - 1] = null;
        size--;
    }

    /**
     * @return - an array containing all the objects in the collection
     */
    public Object[] getAll(){
        return collection;
    }

    /**
     * @return - the number of elements in the collection
     */
    public int getSize(){
        return size;
    }

    public Iterator iterator(){
        return new Iterator(){
            private int index = 0;
            public boolean hasNext(){
                return index < size;
            }
            public Object next(){
                return collection[index++];
            }
        };
    }
}
