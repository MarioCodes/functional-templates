package es.msanchez.commons.java.builders;

import lombok.extern.slf4j.Slf4j;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;

@Slf4j
class Randomizer<TYPE> extends RandomsImpl {

    /**
     * Fills all the fields, for all elements of {@code types} with natural, random data for every field.
     *
     * @param types list with all instances to fill.
     */
    void fill(final List<TYPE> types) {
        types.forEach(this::fill);
    }

    /**
     * Fills all the fields of {@code type} with natural, random data for every field.
     *
     * @param type instance to fill
     */
    void fill(final TYPE type) {
        final List<Field> fields = Arrays.asList(type.getClass().getDeclaredFields());
        log.trace("Found '{}' fields for class '{}'", fields.size(), type.getClass());
        fields.forEach(field -> this.randomizeField(type, field));
    }

    private void randomizeField(final TYPE type,
                                final Field field) {
        try {
            final String fieldName = field.getType().getName();
            final Object randomValue = this.getCorrectRandom(fieldName);
            field.setAccessible(true);
            field.set(type, randomValue);
        } catch (final IllegalAccessException ex) {
            log.error("Error on fill field with reflection", ex);
        }
    }

    /**
     * @param fieldName -
     * @return -
     * @throws UnsupportedOperationException When there's no implementation to fill a data type with a random value.
     *                                       Decided to throw an exception as it will be easier to see and find when it happens,
     *                                       as compared to returning a null value.
     */
    private Object getCorrectRandom(final String fieldName) {
        final Object random;
        if (fieldName.contains("Long")) {
            random = this.randomPositiveLong();
        } else if (fieldName.contains("String")) {
            random = this.randomAlphanumeric();
        } else if (fieldName.contains("Integer")) {
            random = this.randomPositiveInteger();
        } else if (fieldName.contains("Boolean")) {
            random = this.randomBoolean();
        } else if (fieldName.contains("Double")) {
            random = this.randomDouble();
        } else if (fieldName.contains("Float")) {
            random = this.randomFloat();
        } else if (fieldName.contains("List")) {
            random = new ArrayList<>();
        } else if (fieldName.contains("Set")) {
            random = new HashSet<>();
        } else {
            log.error("Tried to fill the field type '{}' but it's not known", fieldName);
            throw new UnsupportedOperationException("Tried to automatically fill a field for a Builder, " +
                    "but the type is not known. Please implement it. ");
        }
        return random;
    }

}
