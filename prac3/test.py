import unittest
from CircleCI.main import to_upper

class MyTestCase(unittest.TestCase):
    def test_to_upper(self):
        name='Tannu'
        upper_name=to_upper(name)
        self.assertEqual(upper_name,'TANNU')

if __name__ == '__main__':
    unittest.main()
