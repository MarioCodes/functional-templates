package es.codes.mario.prueba.dao;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.NoRepositoryBean;

/**
 * Spring JPA for database access. Extend this class from an interface to use it.
 *
 * @param <TYPE> Entity we want the DAO for.
 */
@NoRepositoryBean
interface RawDao<TYPE> extends CrudRepository<TYPE, Long> {
}
