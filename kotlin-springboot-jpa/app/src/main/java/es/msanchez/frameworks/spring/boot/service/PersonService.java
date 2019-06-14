package es.msanchez.frameworks.spring.boot.service;

import es.msanchez.frameworks.spring.boot.dao.PersonDao;
import es.msanchez.frameworks.spring.boot.entity.Person;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Slf4j
@Service
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
