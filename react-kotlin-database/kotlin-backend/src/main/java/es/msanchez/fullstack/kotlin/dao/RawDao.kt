package es.msanchez.fullstack.kotlin.dao

import org.springframework.data.repository.CrudRepository
import org.springframework.data.repository.NoRepositoryBean

@NoRepositoryBean
interface RawDao<TYPE> : CrudRepository<TYPE, Long>