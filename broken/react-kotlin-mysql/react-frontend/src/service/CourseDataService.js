import axios from 'axios'

const INSTRUCTOR = "msanchez"
const COURSE_API_URL = "http://localhost:34831"
const INSTRUCTOR_API_URL = `${COURSE_API_URL}/courses/instructors/${INSTRUCTOR}`

class CourseDataService {

    retrieveAllCourses(name) {
        return axios.get(`${INSTRUCTOR_API_URL}`);
    }

    retrieveCourse(id) {
        return axios.get(`${COURSE_API_URL}/courses/${id}`)
    }

    deleteCourse(id) {
        return axios.delete(`${COURSE_API_URL}/courses/${id}`);
    }

}

export default new CourseDataService()
