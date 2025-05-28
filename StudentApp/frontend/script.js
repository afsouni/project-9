const API_URL = '/api/students';

    async function loadStudents() {
      try {
        const res = await axios.get(API_URL);
        const tbody = document.getElementById('student-body');
        tbody.innerHTML = res.data
          .map(s => `
            <tr>
              <td>${s.id}</td>
              <td>${s.firstName} ${s.lastName}</td>
              <td>${s.address}</td>
              <td>
                <button class="btn btn-sm btn-danger" onclick="deleteStudent(${s.id})">Delete</button>
              </td>
            </tr>
          `).join('');
      } catch (err) {
        console.error('GET failed', err);
      }
    }

    document.getElementById('student-form').addEventListener('submit', async e => {
      e.preventDefault();
      const id = document.getElementById("studentId").value;
      const fullName = document.getElementById("studentName").value.trim();
      const address = document.getElementById("studentAddress").value;
      const [firstName, ...lastParts] = fullName.split(" ");
      const lastName = lastParts.join(" ");

      const student = {
        id: parseInt(id),
        firstName,
        lastName,
        address
      };

      try {
        await axios.post(API_URL, student);
        loadStudents();
        bootstrap.Modal.getInstance(document.getElementById('addStudentModal')).hide();
        e.target.reset();
      } catch (err) {
        console.error('POST failed:', err);
        alert("Error adding student.");
      }
    });

    async function deleteStudent(id) {
      if (!confirm("Are you sure you want to delete this student?")) return;

      try {
        await axios.delete(`${API_URL}/${id}`);
        loadStudents();
      } catch (err) {
        console.error("Delete failed:", err);
        alert("Error deleting student.");
      }
    }

    loadStudents();