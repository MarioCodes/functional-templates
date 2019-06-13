package es.msanchez.frameworks.java.spring.boot.service;

import es.msanchez.frameworks.java.spring.boot.dao.PersonDao;
import es.msanchez.frameworks.java.spring.boot.entity.Person;
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
