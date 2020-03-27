docker-compose up -d --build --force-recreate
docker image prune --filter label=stage=builder --force
