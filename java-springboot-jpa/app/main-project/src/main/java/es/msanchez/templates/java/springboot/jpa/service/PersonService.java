package es.msanchez.templates.java.springboot.jpa.service;

import es.msanchez.templates.java.springboot.jpa.dao.PersonDao;
import es.msanchez.templates.java.springboot.jpa.entity.Person;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

import java.util.Optional;

@Slf4j
@Component
public class PersonService {

    private final PersonDao personDao;

    public PersonService(final PersonDao personDao) {
        this.personDao = personDao;
    }

    public boolean isValid(final String name) {
        final Optional<Person> exists = this.personDao.findOneByName(name);
        return !exists.isPresent();
    }

    public void save(final Person person) {
        this.personDao.save(person);
    }

}
