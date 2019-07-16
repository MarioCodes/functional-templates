package es.msanchez.templates.java.vertx.builder.implementation;

import es.msanchez.commons.java.builders.AbstractBuilder;
import es.msanchez.templates.java.vertx.entity.Person;

public class PersonBuilder extends AbstractBuilder<PersonBuilder, Person> {

    /*
     * Methods to copy, paste and modify in all *Builder instances.
     */

    public static PersonBuilder getInstance() {
        return new PersonBuilder();
    }

    @Override
    protected Person instantiate() {
        return new Person();
    }

    @Override
    protected PersonBuilder builder() {
        return this;
    }

    /*
     * Custom 'with' methods per builder.
     */

    public PersonBuilder withAge(final Integer age) {
        super.with(p -> p.setAge(age));
        return this;
    }

    public PersonBuilder withName(final String name) {
        super.with(p -> p.setName(name));
        return this;
    }

}
