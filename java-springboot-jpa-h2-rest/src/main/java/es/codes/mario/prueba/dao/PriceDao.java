package es.codes.mario.prueba.dao;

import es.codes.mario.prueba.entity.Price;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.time.ZonedDateTime;
import java.util.Optional;

public interface PriceDao extends RawDao<Price> {

    /**
     * <pre>
     * This {@link Query} gives back a <b>single</b> matching {@link Price} if there's one to be found. If there's more than a single
     * {@link Price} it will filter the results by higher {@link Price#getPriority()} field. This was done at database level to
     * improve the App's performance.
     *
     * This is also cached to improve performance for large data sets.
     * </pre>
     *
     * @param date      valid {@link ZonedDateTime}
     * @param productId has to be positive.
     * @param brandId   has to be positive.
     * @return {@link Optional#empty()} if no matching data is found inside the database or {@link Price} if found.
     */
    @Query("SELECT p FROM Price p " +
            "WHERE p.productId = :productId AND p.brandId = :brandId AND :date BETWEEN p.startDate AND p.endDate " +
            "AND p.priority = " +
            "(SELECT MAX(pp.priority) FROM Price pp WHERE pp.productId = :productId AND pp.brandId = :brandId AND :date BETWEEN pp.startDate AND pp.endDate)")
    @Cacheable(value = "prices")
    Optional<Price> findOneByZonedDateTimeAndProductIdAndBrandId(@Param("date") final ZonedDateTime date,
                                                                 @Param("productId") final Long productId,
                                                                 @Param("brandId") final Long brandId);

    /**
     * I didn't bind this method as there's no create or update for this test. If this was real, we'd need to call this when inserting something
     * into the database or when changing database data to force an update. The cache has a TTL of 6 minutes anyway.
     */
    @CacheEvict(value = "prices", allEntries = true)
    default void refreshAllPrices() {
        // this clears all the values for the cache 'prices' when called. This cache is configured inside 'ehcache.xml' file.
    }

}
