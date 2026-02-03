sp_help course_info;

select zip_code, city, state as state_name
from zipcode_info;

select distinct state
from zipcode_info;

select student_id,
       student_first_name + ' ' + student_last_name as name
from student_info;

select zip_code + ', ' + city + ', ' + state as address
from zipcode_info;

select course_name
from course_info;

select course_name, cost
from course_info;

select *
from course_info;

select instructor_last_name, zip_code
from instructor_info;

select distinct zip_code
from student_info;

select student_first_name, student_last_name
from student_info;

select city, state, zip_code
from zipcode_info;

