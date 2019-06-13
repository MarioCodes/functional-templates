package es.msanchez.frameworks.java.spring.boot.rest;

import es.msanchez.frameworks.java.spring.boot.entity.Person;
import es.msanchez.frameworks.java.spring.boot.exception.DataTransferException;
import es.msanchez.frameworks.java.spring.boot.service.PersonService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Slf4j
@RestController
@RequestMapping("/person")
public class PersonRestResource {

    private final PersonService personService;

    @Autowired
    public PersonRestResource(final PersonService personService) {
        this.personService = personService;
    }

    @GetMapping("create/{name}")
    public void createPerson(@PathVariable("name") final String name) {
        if (!this.personService.isValid(name)) {
            log.error("The given name '{}' is not valid", name);
            throw new DataTransferException("Tried to create a person with a wrong name.");
        }

        this.personService.save(this.create(name));
        log.info("created person with name '{}'", name);
    }

    private Person create(final String name) {
        final Person person = new Person();
        person.setName(name);
        return person;
    }

}
